package events

// Struct ánh xạ OrderFailedEvent từ JSON
type OrderItemEvent struct {
	ProductID string `json:"productId"`
	Quantity  int    `json:"quantity"`
}

type OrderFailedEvent struct {
	OrderID      string           `json:"orderId"`
	UserID       string           `json:"userId"`
	OrderItemList []OrderItemEvent `json:"orderItemList"`
}
