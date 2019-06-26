# ShopWebApp
.NET Core Web API application for demo only.

## APIs for products
POST /api/products: Saves a new product to the /assets/products/ directory as a JSON file, named by its product ID.
GET /assets/products/<id>.json: Gets the static json file for the product with the given ID.

## APIs for shopping cart
GET /api/cart: Get the product items in shopping cart.
POST /api/cart/add: Add new product item with quantity (>0) into shopping cart.
POST /api/cart/remove: Remove product item with quantity (>0) from shopping cart.
POST /api/cart/update: Update product item and quantity (>=0) from shopping cart.
POST /api/cart/clear: Empty all product items with quantity from shopping cart.

## Unit test project
Unite test project is demo only, which only implemented few test methods.

## IMPORTANT NOTES!
This is for test purpose. Not all the functionalities are implemented.


