package events

type OrderCompletedEvent struct {
	UserId  string `json:"userId"`
	OrderId string `json:"orderId"`
}