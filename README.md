# Projektarbete i kursen DT191G - Webbutveckling i .NET

## Models

### CustomerOrder
Denna modell styr innehållet i tabellen som lagrar data om kundens personliga information, status på order samt ordernummer. Ordernummer returneras till frontend-appen för att användas i detailedOrder.

* OrderId
* Ordernumber
* Firstname
* Lastname
* Email
* Phonenumber
* Adress
* ZipCode
* City
* PurchaseDate
* TotalPrice
* ShippingCost
* IsPacked
* IsShipped

## Detailed Order
Två tabeller lagras i denna databas - detailedorder och articles. Id i detailedOrder blir fk i articles. 

### DetailedOrder
* Id
* OrderNumber
* Articles (av typen list)

### Articles
* Id
* ArticleNumber
* ProductTitle
* Amount
* DetailedOrderId (id från DetailedOrder)

## Product
Här lagras all data kring produkter. Kategorier och märken hämtas och skrivs ut i formulärdata som alternativ. 

* Id
* ArticleNumber
* AmountInStock
* Title
* ProductDescription
* ImageName (sökväg till bild lagras)
* AltText
* Category
* Weight
* Price
* Brand
* Created
* IsInStartkit
* ImageFile (not mapped)

## ProductCateogory
Lagring av produktkategorier

* Id
* CategoryName

## ProductBrand
Lagring av produktmärken

* Id
* BrandName


































