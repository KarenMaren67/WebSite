(function ($,JSON) {

	var cartCookieName = "cartCookieName";

	function addToCart(event) {
		var self = $(this);

		var productId = self.data("product-id");
		var count = self.closest(".input-group").children("input")[0].value;

		var cartArr = getCartArray();

		cartArr.push({ "ProductId": productId, "Count": count });

		pastToCookie(cartCookieName, JSON.stringify(cartArr));

		updateCartCount();
	}

	function getOrCreateCookie(name) {
		var cart = $.cookie(name);
		if (cart === undefined) {
			cart = "[]";
			$.cookie(name, cart);
		}
		return cart;
	}

	function pastToCookie(name, value) {
		$.cookie(name, value);
	}

	function getCartArray() {
		var cartStr = getOrCreateCookie(cartCookieName);

		return  JSON.parse(cartStr);
	}

	function updateCartCount() {
		
		var cartArr = getCartArray();

		var countInCart = cartArr.length;
		$(".badge").html(countInCart);
	}

	$(".to-cart").click(addToCart);
	updateCartCount();

})(window.$,window.JSON);