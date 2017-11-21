﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Astu
{
    /// <summary>
    /// Приказ о смене направления подготовки (специальности)
    /// </summary>
    public class DirectionChangingOrder: StudentOrderBase
    {
        public DirectionChangingOrder() : base ()
        {
            OrderTypeId = "0011";
            Comment = "Переведен на другое направление подготовки";
        }

        /// <summary>
        /// Дата начала обучения
        /// </summary>
        [DbFieldInfo("DAT_START", DbFieldType.DateTime)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Курс, на который зачислен студент
        /// </summary>
        [DbFieldInfo("NEWKURS", DbFieldType.Integer)]
        public int NewCourse { get; set; }
        
        /// <summary>
        /// Идентификатор направления подготовки
        /// </summary>
        [NavigationProperty(typeof(Direction))]
        [DbFieldInfo("SPC")]
        public string DirectionId { get; set; }

        /// <summary>
        /// Идентификатор группы, куда зачислен студент
        /// </summary>
        [NavigationProperty(typeof(Group))]
        [DbFieldInfo("GRP")]
        public string GroupId { get; set; }
        
        /// <summary>
        /// Направление подготовки
        /// </summary>
        public Direction Direction
        {
            get
            {
                return Astu.Directions.FirstOrDefault(d => d.Id == DirectionId);
            }
            set
            {
                DirectionId = value?.Id;
            }
        }

        /// <summary>
        /// Группа, в которую зачислен студент
        /// </summary>
        public Group Group
        {
            get
            {
                return Astu.Groups.FirstOrDefault(g => g.Id == GroupId);
            }
            set
            {
                GroupId = value?.Id;
            }
        }

    }
}
