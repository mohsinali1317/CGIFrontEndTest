function Cart(data) {
    var self = this;

    this.title = data.title;
    this.manufacturerCountry = data.manufacturerCountry;
    this.price = data.price;
    this.amount = ko.observable(1);


}