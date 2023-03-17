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

## AboutUs
Lagring av artiklar till webshoppens "Om Oss" - sida

* Id
* Title
* Text
* ImageName
* AltText
* ImageFile

## Recipe
Två tabeller lagras i denna databas - recipe och ingredients. Id i recipe blir fk i ingredients. 

### Recipe
* Id
* Title
* Description
* ImageName
* AltText
* ImageFile
* Ingredients (av typen list)

### Ingredients
* Id
* Name
* Quantity
* Unit
* RecipeId (id från Recipe)


## Möjliga anrop från extern källa

| Metod         | Ändpunkt        | Beskrivning   
| ------------- | -------------   | --------    |
| GET           | /getallproducts  | Hämtar alla produkter            |
| GET           | /getlatestproducts  | Hämtar senaste 6 produkterna            |
| GET           | /getproductbyid/:id  | Hämtar unik produkt           |
| GET           | /getstartkitproduct  | Hämtar alla produkter i startkit           |
| GET           | /getallrecipes | Hämtar alla recept           |
| GET           | /getallarticles  | Hämtar alla artiklar till om-oss-sidan            |
| POST           | /customerorder/create  | Lägger kundorder           |
| POST           | /detailed<order/create  | Lägger detaljerad produktorder kopplat till customerorder           |































