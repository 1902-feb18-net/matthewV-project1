--Drop Database project0;
--go

--Create Database project0;
--go

--Create schema project0;
--GO

CREATE TABLE customer
(
	id int Unique Not Null Identity,
	first_name nvarchar(255) Not Null,
	last_name nvarchar(255) Not Null,
	email nvarchar(255),	
	address_id int, 
	store_id int NOT NULL, --customer has a default store
	Constraint customerPK Primary Key (id)
);

CREATE TABLE orders
(
	id int Unique Not Null Identity,
	customer_id int Not Null,
	store_id int Not Null,
	total_price decimal(8,2), 
    address_id int, --null address means carry out, not delivery
	order_time datetime2 Not Null, --set to time of order creation
	Constraint ordersPK Primary Key (id)
	--a customer CAN place multiple orders to a store, as long as they are >2 hours apart
);

CREATE TABLE order_items  
(
	 id int Not Null Unique Identity,
	 order_id  int Not Null,
	 pizza_id  int Not Null,
	 quantity  int Not Null, --multiples of a pizza can be in an order
	 Constraint PK Primary Key (id),
	 Constraint order_pizza_unique Unique (order_id, pizza_id) 
	 --number of pizza in an order is specified by quantity, not another row with the same id combo
);

CREATE TABLE store  
(
	 id int Unique Not Null Identity,
	 name nvarchar(255) Not Null,
	 address_id int Not Null,  --store's physical location
	 Constraint storePK Primary Key (id)
);

CREATE TABLE pizza  
(
	 id int Unique Not Null Identity,
	 price decimal(5,2) Not Null, --assumed pizza won't cost more than 999.99
	 Constraint pizzaPK Primary Key (id)
);

CREATE TABLE ingredients  
(
	 id int Unique Not Null Identity,
	 name nvarchar(255) Not Null,
	 Constraint ingredientsPK Primary Key (id)
);

CREATE TABLE pizza_ingredients  
(
    id int Not Null Unique Identity, 
	pizza_id int Not Null,
	ingredients_id int Not Null,
    quantity int Not Null, --number of the ingredient required to make the pizza
	Constraint pizza_ingredientsPK Primary Key (id),
    Constraint pizza_ingredients_unique Unique (pizza_id, ingredients_id)
	--number of ingredients in a pizza is specified by quantity, not another row with the same id combo
);


CREATE TABLE store_ingredients  
(
	 id int Unique Not Null Identity,
	 store_id  int Not Null,
	 ingredients_id  int Not Null,
	 quantity  int Not Null, --number of ingredients in the store's inventory
	 Constraint store_ingredientsPK Primary Key (id),
	 Constraint store_ingredients_unique Unique (store_id, ingredients_id)
	 --number of ingredients in a store's inventory is specified by quantity, not another row with the same id combo
);

CREATE TABLE address  
(
	 id int Unique Not Null Identity,
	 street  nvarchar(255) Not Null,
	 city  nvarchar(255),
	 state  nvarchar(255),
	 country nvarchar(255) Not Null,
	 zipcode  nvarchar(255),
	 Constraint addressPK Primary Key (id)
);

ALTER TABLE customer ADD Constraint FK_customer_store FOREIGN KEY (store_id) REFERENCES store (id);

ALTER TABLE customer ADD Constraint FK_customer_address FOREIGN KEY (address_id) REFERENCES address (id);

ALTER TABLE orders ADD Constraint FK_orders_store FOREIGN KEY (store_id) REFERENCES store (id);

ALTER TABLE orders ADD Constraint FK_orders_customer FOREIGN KEY (customer_id) REFERENCES customer (id);

ALTER TABLE orders ADD Constraint FK_orders_address FOREIGN KEY (address_id) REFERENCES address (id);

ALTER TABLE order_items ADD Constraint FK_orderitems_orders FOREIGN KEY (order_id) REFERENCES orders (id);

ALTER TABLE order_items ADD Constraint FK_orderitems_pizza FOREIGN KEY (pizza_id) REFERENCES pizza (id);

ALTER TABLE store ADD Constraint FK_store_address FOREIGN KEY (address_id) REFERENCES address (id);

ALTER TABLE pizza_ingredients ADD Constraint FK_pizzaingredients_pizza FOREIGN KEY (pizza_id) REFERENCES pizza (id);

ALTER TABLE pizza_ingredients ADD Constraint FK_pizzaingredients_ingredients FOREIGN KEY (ingredients_id) REFERENCES ingredients (id);

ALTER TABLE store_ingredients ADD Constraint FK_storeingredients_ingredients FOREIGN KEY (ingredients_id) REFERENCES ingredients (id);

ALTER TABLE store_ingredients ADD Constraint FK_storeingredients_store FOREIGN KEY (store_id) REFERENCES store (id);

GO

--Create Trigger NoSameCustomerStoreOrderWithinTwoHours on project0.orders
--For Insert
--AS
--Begin
--       Begin Try
--		IF EXISTS (SELECT * 
--					FROM Orders
--					Where (Select inserted.customer_id From inserted) = orders.customer_id
--					AND (Select inserted.store_id From inserted) = orders.store_id
--					AND ( (DATEDIFF(hour, orders.order_time, (Select inserted.order_time From inserted) ) >= 2 )	
--							OR DATEDIFF(hour, (Select inserted.order_time From inserted), orders.order_time) >= 2 ) )	
--		Begin
--			RAISERROR ('A customer cannot order from the same store within 2 hours', 16, 1);  
--		END
--		--else is insert normally
--		End Try
--		Begin Catch
--			Print Error_Message();
--		End Catch
--End;

--GO 

--not finished
--Create Trigger OrderDecreasesstore_ingredients on project0.orders
--For Insert
--AS
--Begin
--		Update store_ingredients
--		SET store_ingredients.quantity = store_ingredients.quantity - (Select order_items.quantity From inserted join order_items on inserted.id = order_items.order_id)
--		WHERE store_ingredients.id IN (Select store_ingredients.id FROM inserted join store on inserted.store_id = store.id
--						join store_ingredients on store.id = store_ingredients.store_id)
--END;

