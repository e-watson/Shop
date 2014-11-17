function BasketDetailLine(id, name, quantity, totalPrice) {
	this.id = id;
	this.name = name;
	this.quantity = quantity;
	this.totalPrice = totalPrice.toFixed(2);
};