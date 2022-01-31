// Copyright (c) 2022 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace MTConnect.Devices.Events
{
    /// <summary>
    /// A SensorAttachment is an Event defining an Attachment between a sensor and an entity.
    /// </summary>
    public class SensorAttachmentDataItem : DataItem
    {
        public const DataItemCategory CategoryId = DataItemCategory.EVENT;
        public const string TypeId = "SENSOR_ATTACHMENT";
        public const string NameId = "sensorAttachment";


        public SensorAttachmentDataItem()
        {
            DataItemCategory = CategoryId;
            Type = TypeId;
        }

        public SensorAttachmentDataItem(string parentId)
        {
            Id = CreateId(parentId, NameId);
            DataItemCategory = CategoryId;
            Type = TypeId;
            Name = NameId;
        }
    }
}