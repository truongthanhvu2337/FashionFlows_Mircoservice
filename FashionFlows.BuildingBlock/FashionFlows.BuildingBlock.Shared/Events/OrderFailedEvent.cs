﻿using FashionFlows.BuildingBlock.Domain.Events.Interface;

namespace FashionFlows.BuildingBlock.Domain.Events;

public class OrderFailedEvent : IOrderFailedEvent
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItemEvent> OrderItemList { get; set; }
}
