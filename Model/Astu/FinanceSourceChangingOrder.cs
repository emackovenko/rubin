﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Приказ о смене источника финансирования(категории обучения)
    /// </summary>
    public class FinanceSourceChangingOrder: StudentOrderBase
    {
        public FinanceSourceChangingOrder() : base ()
        {
            OrderTypeId = "0012";
            Comment = "Переведен на ";
        }
        
        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }
        
        /// <summary>
        /// Идентификатор источника финансирования (категории обучения)
        /// </summary>
        [NavigationProperty(typeof(FinanceSource))]
        [DbFieldInfo("KOB")]
        public string FinanseSourceId { get; set; }


        /// <summary>
        /// Источник финансирования (категория обучения)
        /// </summary>
        public FinanceSource FinanceSource
        {
            get
            {
                return Astu.FinanceSources.FirstOrDefault(fs => fs.Id == FinanseSourceId);
            }
            set
            {
                FinanseSourceId = value?.Id;
            }
        }

    }
}
