﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using CommonMethods.Documents;
using GemBox.Document;
using GemBox.Document.Tables;
using ResourceLibrary.Documents;
using CommonMethods.TypeExtensions.exGemBox;
using CommonMethods.TypeExtensions.exString;
using MoreLinq;


namespace Admission.ViewModel.Documents
{
	public class EnrollmentOrderDocument : OpenXmlDocument
	{

		public EnrollmentOrderDocument(EnrollmentOrder order)
		{
			_order = order;
            DocumentType = OpenXmlDocumentType.Document;
		}

		EnrollmentOrder _order;

		public override void CreatePackage(string fileName)
		{
            // Извлекаем шаблон
            var document = DocumentModel.Load(DocumentTemplate.ExtractDoc("EnrollmentOrder"));

            // Подготовка стилей
            var underlinedCharacterFormat = new CharacterFormat
            {
                UnderlineColor = Color.Black,
                UnderlineStyle = UnderlineType.Single,
				Size = 12.0
            };

			var simpleTextFormat = new CharacterFormat
			{
			  Size = 12.0,
			  FontName = "Times New Roman"
			};
													   

            // Вставляем текст на закладки
            document.InsertToBookmark("OrderDate",
                ((DateTime)_order.Date).ToString("«dd» MMMM yyyy г."), simpleTextFormat);
            document.InsertToBookmark("OrderNumber", _order.Number, underlinedCharacterFormat);
            document.InsertToBookmark("TrainingBeginDate",
                ((DateTime)_order.EnrollmentProtocols.First().TrainingBeginDate).ToString("dd.MM.yyyy"));
            document.InsertToBookmark("EducationForm", ChangeToAccusative(_order.EducationForm.Name), simpleTextFormat);

            string auxOrderReason = "оригинал документа об образовании";
            if (_order.FinanceSource.Id == 2)
            {
                auxOrderReason = "договоры об оказании платных образовательных услуг";
            }
            document.InsertToBookmark("AuxOrderReason", auxOrderReason);

            if (_order.FinanceSource.Id == 2)
            {
                document.InsertToBookmark("IsPaylineEducation", "не ", simpleTextFormat);
            }

            // Получаем строку с протоклами
            string namesProtocol = string.Empty;
            foreach (var p in _order.EnrollmentProtocols.OrderBy(p => p.Number))
            {
                namesProtocol += string.Format(" № {0} от {1},", p.Number,
                    ((DateTime)p.Date).ToString("dd.MM.yyyy г."));
            }
            namesProtocol = namesProtocol.TrimEnd(',');

            document.InsertToBookmark("EnrollmentProtocols", namesProtocol, simpleTextFormat);
					   
			var reasons = document.Sections[3].Clone(true);
			document.Sections.Remove(document.Sections[3]);


			// Вставляем таблицы по протоколам и заполняем их
			var tableTemplate = document.Sections[2];
			for (int i = 0; i < _order.EnrollmentProtocols.Count - 1; i++)
			{
				document.Sections.Add(tableTemplate.Clone(true));
			}

			int tableIndex = 2;
			foreach (var protocol in _order.EnrollmentProtocols)
			{
				var table = document.Sections[tableIndex].GetChildElements(true, ElementType.Table).Cast<Table>().FirstOrDefault();


                // Направление подготовки
                string dirStrHelper = "программа бакалавриата";
                if (protocol.CompetitiveGroup.Direction.DirectionProfiles.Count() > 1)
                {
                    dirStrHelper = "совокупность программ бакалавриата";
                }

				string directionString = string.Format("Направление подготовки\n{0} {1}, {2}\n",
					protocol.CompetitiveGroup.Direction.Code, protocol.CompetitiveGroup.Direction.Name,
                    dirStrHelper);
                foreach (var profile in protocol.CompetitiveGroup.Direction.DirectionProfiles)
                {
                    directionString += string.Format("«{0}», ", profile.Name);
                }
                directionString = directionString.Trim(' ');
                directionString = directionString.Trim(',');

				table.Rows[1].Cells[0].Content.LoadText(directionString, simpleTextFormat);

				// Срок освоения
				string trainingTime = string.Format("Срок получения образования по программе: {0}", protocol.TrainingTime.AsPeriod());
				table.Rows[2].Cells[0].Content.LoadText(trainingTime, simpleTextFormat);

				// Срок окончания
				string trainingEndDate = string.Format("Срок окончания обучения по образовательной программе: {0}", 
					((DateTime)protocol.TrainingEndDate).ToString("dd MMMM yyyy г."));

                //ебучий костыль: строчка, что выше, выводится в документ только у очников
                if (_order.EducationForm.Id == 2)
                {
                    trainingEndDate = string.Empty;
                }
				table.Rows[3].Cells[0].Content.LoadText(trainingEndDate, simpleTextFormat);

				// Список
				var rowTemplate = table.Rows[5];
				foreach (var claim in protocol.EnrollmentClaims.OrderBy(ec => ec.StringNumber))
				{
					var tableRow = rowTemplate.Clone(true);

					// №
					tableRow.Cells[0].Content.LoadText(claim.StringNumber.ToString(), simpleTextFormat);	  

					// ФИО
					tableRow.Cells[1].Content.LoadText(claim.Claim.Person.FullName, simpleTextFormat);

					// Основание
					tableRow.Cells[2].Content.LoadText(protocol.CompetitiveGroup.FinanceSource.EnrollmentReason, simpleTextFormat);

					// Рег. номер
					tableRow.Cells[3].Content.LoadText(claim.Claim.Number, simpleTextFormat);

					// Сумма баллов	 
					tableRow.Cells[4].Content.LoadText(claim.Claim.TotalScore.ToString(), simpleTextFormat);
					tableRow.Cells[0].CellFormat = new TableCellFormat
					{
					  VerticalAlignment = VerticalAlignment.Center					 
					};
					table.Rows.Add(tableRow);
				}

				// Итог
				var resultRow = table.Rows[4].Clone(true);
				resultRow.Cells[0].Content.LoadText("Итого:", simpleTextFormat);
				resultRow.Cells[1].Content.LoadText(string.Format("{0} - {1}",
                    protocol.CompetitiveGroup.FinanceSource.EnrollmentReason,
                    protocol.EnrollmentClaims.Count), simpleTextFormat);			  
				table.Rows.Add(resultRow);			  

				tableIndex++;
			}

			document.Sections.Add(reasons);
            document.Save(fileName);

        }


		/// <summary>
		/// Возвращает имя формы обучения в винительном (теперь предложном) падеже
		/// </summary>
		/// <param name="educationFormName">имя в оригинале</param>
		/// <returns></returns>
		private string ChangeToAccusative(string educationFormName)
		{
			string str = educationFormName;
			str = str.Replace("ая", "ой");
			return str;
		}

		string ConvertDecimalToTime(decimal value)
		{
			return "4 года 10 месяцев";
		}
	}
}
