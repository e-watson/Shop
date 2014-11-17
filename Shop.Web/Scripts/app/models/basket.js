var Basket = function () {
	this.NOT_FOUND = -1;
	this.VAT = 0.2;

	this.items = [];

	this.total = "";
	this.vat = "";
	this.totalVat = "";
};

Basket.prototype = function () {
	var getItemById = function (id) {
		var index = this.NOT_FOUND;
		for (var i = 0, len = this.items.length; i < len; i++) {
			if (this.items[i].id == id) {
				index = i;
				break;
			}
		}
		return index;
	},

	calcTotal = function () {
		var t = 0, v = 0;

		for (var i = 0, len = this.items.length; i < len; i++) {
			t += (this.items[i].quantity * this.items[i].price);
			v += (this.VAT * (this.items[i].quantity * this.items[i].price));
		}

		this.total = t.toFixed(2);
		this.vat = v.toFixed(2);
		this.totalVat = (t + v).toFixed(2);
	},

	addItem = function (id, name, price, quantity) {
		var i = getItemById.call(this, id);
		if (i == this.NOT_FOUND) {
			this.items.push(new BasketItem(id, name, price, quantity));
		} else {
			this.items[i].quantity += quantity;
		}

		calcTotal.call(this);
	},

	deleteItem = function (id) {
		var i = getItemById(id);
		if (i != this.NOT_FOUND) {
			this.items.splice(i, 1);
		}
	},

	getBasketInfo = function () {
		var qsum = 0, psum = 0;

		for (var i = 0, len = this.items.length; i < len; i++) {
			qsum += this.items[i].quantity;
			psum += (this.items[i].price * this.items[i].quantity);
		}

		return new BasketInfo(qsum, psum);
	},

	getBasketDetails = function () {
		var details = new BasketDetails([], 0, 0, 0);
		for (var i = 0, len = this.items.length; i < len; i++) {
			details.lines.push(new BasketDetailLine(this.items[i].id, this.items[i].name, this.items[i].quantity,
				(this.items[i].quantity * this.items[i].price)));

			details.total += (this.items[i].quantity * this.items[i].price);
			details.vat += (this.VAT * (this.items[i].quantity * this.items[i].price));
		}

		details.totalVat = (details.total + details.vat).toFixed(2);
		details.total = details.total.toFixed(2);
		details.vat = details.vat.toFixed(2);

		return details;
	},

	serialize = function () {
		var strBasket = "", template = "";
		for (var i = 0, len = this.items.length; i < len; i++) {
			template = strBasket == "" ? "{0}{1}-{2}" : "{0},{1}-{2}";
			strBasket = template.format(strBasket, this.items[i].id, this.items[i].quantity);
		}

		return strBasket;
	};


	return {
		addItem: addItem,
		deleteItem: deleteItem,
		getBasketInfo: getBasketInfo,
		getBasketDetails: getBasketDetails,
		serialize: serialize
	};
}();