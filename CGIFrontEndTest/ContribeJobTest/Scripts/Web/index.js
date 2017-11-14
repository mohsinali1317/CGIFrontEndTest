var viewModel = null;
$(function () {
    viewModel = new AppViewModel();
    viewModel.fetchAllFoodItems();
    ko.applyBindings(viewModel);
});


function AppViewModel() {

    //declaration of variables 
    var self = this;
    self.rawFoodItems = ko.observableArray([]).withIndex('id');
    self.inCart = ko.observableArray([]).withIndex('id');
    self.responseFromServerArray = ko.observableArray([]).withIndex('id');
    self.searchString = ko.observable(null);

    self.searchString.subscribe(function (newVal) {

        if (newVal == null || newVal == "" || newVal == 0) {
            return self.fetchAllFoodItems();
        }
        else {
            return self.searchFoodItem(newVal);
        }
    })


    self.totalPricePaid = ko.computed(function () {

        var totalPricePaid = 0;
        ko.utils.arrayForEach(self.responseFromServerArray(), function (item) {

            totalPricePaid += item.price * item.amount;
        })
        return totalPricePaid;
    });

    self.totalPrice = ko.computed(function () {

        var totalPrice = 0;
        ko.utils.arrayForEach(self.inCart(), function (item) {

            totalPrice += item.price * item.amount();
        })
        return totalPrice;
    });


    //Fetching data from server
    self.fetchAllFoodItems = function () {

        $.ajax({
            type: 'GET',
            url: apiUrl + '/FoodStore/GetAll',
            contentType: false,
            processData: false
        }).done(function (result) {

            var mapped = $.map(result, function (item) { return new FoodItem(item) });
            self.rawFoodItems(mapped);

        }).fail(function (error) {
            console.log(error);
        });

    }

    //Search functionality
    self.searchFoodItem = function (newVal) {

        var d = {
            searchString: newVal
        }

        $.ajax({
            type: 'GET',
            url: apiUrl + '/FoodStore/GetFoodItems',
            contentType: "application/json; charset=utf-8",
            data: d
        }).done(function (result) {

            var mapped = $.map(result, function (item) { return new FoodItem(item) });
            self.rawFoodItems(mapped);

        }).fail(function (error) {
            console.log(error);
        });

    }

    //Adding book to Cart
    self.addToCart = function (foodItem) {

        var alreadyInCart = false;
        ko.utils.arrayForEach(self.inCart(), function (item) {

            if (item.title == foodItem.title && item.author == foodItem.author) {
                var count = item.amount();
                alreadyInCart = true;
                if (foodItem.inStock <= count) {
                    alert("Cannot add more item as they are not in stock");
                    return;
                }

                count++;
                item.amount(count);
                
            }

            if (alreadyInCart)
                return;
        });

        if (!alreadyInCart) {

            if (foodItem.inStock <= 0) {
                return;
            }

            var cart = new Cart(foodItem);
            self.inCart.push(cart);
        }
    }

    //Removing book from Cart
    self.removeFromCart = function (cart) {

        var remove = false;
        ko.utils.arrayForEach(self.inCart(), function (item) {

            if (item.title == cart.title) {
                var count = item.amount();

                if (count > 1) {
                    count--;
                    item.amount(count);
                } else {
                    remove = true;
                }
            }
        });

        if (remove) {
            self.inCart.remove(function (foodItem) {
                return foodItem == cart;
            });
        }
    }

    //Reseting data on screen
    self.resetData = function () {
        self.responseFromServerArray.removeAll();
        self.inCart.removeAll();
    }

    //Placing order of food items selected in cart
    self.placeOrder = function () {

        var cart = [];
        ko.utils.arrayForEach(self.inCart(), function (item) {

            cart.push({
                title: item.title,
                manufacturerCountry: item.manufacturerCountry,
                price: item.price,
                amount: item.amount
            })
        })

        $.ajax({
            type: 'POST',
            url: apiUrl + '/FoodStore/PlaceOrder',
            data: { '': cart },
            dataType: 'json',
        }).done(function (result) {

            self.inCart.removeAll();
            ko.utils.arrayForEach(result, function (item) {
                self.responseFromServerArray.push(item);
            });
        }).fail(function (error) {
            console.log(error);
        });
    }
}
