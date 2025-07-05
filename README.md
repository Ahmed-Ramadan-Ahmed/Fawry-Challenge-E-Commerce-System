# Fawry-Challenge-E-Commerce-System
Fawry Rise Journey - Full Stack Development Internship Challenge

## ✨ Features

### Core Functionality
- **Product Management**: Define products with name, price, and quantity
- **Expiration Handling**: Support for perishable products with expiration dates
- **Shipping Integration**: Seamless shipping for physical products with weight-based calculations
- **Shopping Cart**: Add products in customer carts
- **Order Processing**: Complete checkout with validation and payment processing
- **Inventory Management**: Automatic stock updates after successful orders

### Product Categories
- **Perishable Products**: Items like cheese and biscuits with expiration dates
- **Shippable Products**: Items like TVs and mobiles that require shipping
- **Digital Products**: Items like scratch cards that don't require shipping

### Validation & Error Handling
- Empty cart validation
- Insufficient balance detection
- Out-of-stock verification
- Expired product checking
- Invalid quantity handling

## 🏗️ Architecture

The system follows object-oriented design principles with clean abstractions:

```
├── Abstract Classes
│   └── Product (base class for all products)
├── Concrete Products
│   ├── PerishableProduct (expires + ships)
│   ├── ShippableProduct (ships only)
│   └── DigitalProduct (no shipping)
├── Core Classes
│   ├── Customer (cart management)
│   ├── CartItem (quantity tracking)
│   └── ECommerceSystem (checkout logic)
├── Interfaces
│   ├── IShippable (shipping contract)
│   ├── IShippingService (shipping abstraction)
|   └── ICalculateShippingFees (Calculate Shipping Fees)
└── Services
    ├── ShippingService (shipping implementation)
    └── CalculateShippingFeesService
```

## 🚀 Getting Started

### Prerequisites
- .NET 6.0 or later
- C# development environment (Visual Studio, VS Code, etc.)

## 📖 Usage

### Basic Example

```csharp
// Create products
var cheese = new PerishableProduct("Cheese", 15.99m, 10, DateTime.Now.AddDays(7), 0.5);
var tv = new ShippableProduct("Smart TV", 599.99m, 5, 15.0);
var scratchCard = new DigitalProduct("Mobile Scratch Card", 10.00m, 100);

// Create customer
var customer = new Customer("Ahmed", 1000.00m);

// Add items to cart
customer.AddToCart(cheese, 2);
customer.AddToCart(scratchCard, 3);
customer.AddToCart(mobile, 1);

// Create e-commerce system and checkout
var shippingService = new ShippingService();
var ecommerceSystem = new ECommerceSystem(shippingService);
ecommerceSystem.Checkout(customer);
```

### Expected Output

```
=== CHECKOUT FOR AHMED ===
ORDER SUMMARY:
2x Cheese = $31.98
3x Mobile Scratch Card = $30.00
1x Smartphone = $799.99
Subtotal: $861.97
Shipping: $6.00
Amount: $867.97
Customer Balance After Payment: $132.03

** Shipment notice **
Shipping 3 items:
2x Cheese 1 kg
1x Smartphone 0.2 kg
Total package weight 1.2 kg
```

## 📦 Product Types

### PerishableProduct
Products that expire and require shipping.

```csharp
var milk = new PerishableProduct(
    name: "Fresh Milk",
    price: 3.50m,
    quantity: 20,
    expirationDate: DateTime.Now.AddDays(5),
    weight: 1.0
);
```

### ShippableProduct
Non-perishable products that require shipping.

```csharp
var laptop = new ShippableProduct(
    name: "Gaming Laptop",
    price: 1299.99m,
    quantity: 3,
    weight: 2.5
);
```

### DigitalProduct
Products that don't require physical shipping.

```csharp
var license = new DigitalProduct(
    name: "Software License",
    price: 99.99m,
    quantity: 1000
);
```

### Product Classes

#### Product (Abstract)
- `string Name` - Product name
- `decimal Price` - Product price
- `int Quantity` - Available quantity
- `bool RequiresShipping` - Whether product needs shipping
- `bool IsExpired` - Whether product has expired
- `double GetWeight()` - Product weight in kg
- `bool IsAvailable(int quantity)` - Check availability

#### Customer
- `string Name` - Customer name
- `decimal Balance` - Customer balance
- `List<CartItem> Cart` - Shopping cart items
- `void AddToCart(Product product, int quantity)` - Add item to cart
- `void ClearCart()` - Empty the cart

#### ECommerceSystem
- `ECommerceSystem(IShippingService service, decimal shippingRate = 5.0m)` - Constructor
- `void Checkout(Customer customer)` - Process customer checkout

## 📋 Examples

The system includes comprehensive examples covering:

1. **Successful Checkout** - Mixed products with shipping
2. **Insufficient Balance** - Customer can't afford order
3. **Empty Cart** - Checkout with no items
4. **Expired Products** - Handling expired perishables
5. **Out of Stock** - Insufficient inventory
6. **Invalid Quantities** - Adding more than available
7. **Heavy Items** - Large orders with shipping calculations
8. **Duplicate Products** - Adding same item multiple times
9. **Digital Only** - Orders without shipping

### Assumptions
- **Currency**: All prices in USD
- **Weight Units**: Kilograms
- **Expiration**: Checked against current system time
- **Default Shipping Rate**: is $5.00 per kg.
- **Shipping**: Calculated by total weight × rate per kg

## Output:
