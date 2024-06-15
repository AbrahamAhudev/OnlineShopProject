-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: onlineshop
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `cartitems`
--

DROP TABLE IF EXISTS `cartitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cartitems` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `cartid` int NOT NULL,
  `productid` int NOT NULL,
  `quantity` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `idcartitems_UNIQUE` (`Id`),
  KEY `productid_idx` (`productid`),
  KEY `fk_cartid_cartitem_idx` (`cartid`),
  CONSTRAINT `fk_cartid_cartitem` FOREIGN KEY (`cartid`) REFERENCES `carts` (`Id`),
  CONSTRAINT `fk_productid_cartitem` FOREIGN KEY (`productid`) REFERENCES `products` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=80 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cartitems`
--

LOCK TABLES `cartitems` WRITE;
/*!40000 ALTER TABLE `cartitems` DISABLE KEYS */;
INSERT INTO `cartitems` VALUES (76,3,13,1),(77,3,10,1),(78,3,16,1),(79,3,15,1);
/*!40000 ALTER TABLE `cartitems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `carts`
--

DROP TABLE IF EXISTS `carts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `carts` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Userid` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  KEY `fk_userid_cart_idx` (`Userid`),
  CONSTRAINT `fk_userid_cart` FOREIGN KEY (`Userid`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carts`
--

LOCK TABLES `carts` WRITE;
/*!40000 ALTER TABLE `carts` DISABLE KEYS */;
INSERT INTO `carts` VALUES (13,13),(3,16),(12,17);
/*!40000 ALTER TABLE `carts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderitems`
--

DROP TABLE IF EXISTS `orderitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderitems` (
  `id` int NOT NULL AUTO_INCREMENT,
  `orderid` int NOT NULL,
  `productid` int NOT NULL,
  `quantity` int NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_orderid_orderitem_idx` (`orderid`),
  KEY `fk_productid_orderitem_idx` (`productid`),
  CONSTRAINT `fk_orderid_orderitem` FOREIGN KEY (`orderid`) REFERENCES `orders` (`Id`),
  CONSTRAINT `fk_productid_orderitem` FOREIGN KEY (`productid`) REFERENCES `products` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=85 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderitems`
--

LOCK TABLES `orderitems` WRITE;
/*!40000 ALTER TABLE `orderitems` DISABLE KEYS */;
INSERT INTO `orderitems` VALUES (71,43,14,1),(72,43,15,1),(73,44,15,1),(74,44,13,1),(79,46,10,1),(80,46,15,1),(81,46,16,1),(83,48,13,2),(84,48,10,1);
/*!40000 ALTER TABLE `orderitems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `Address` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  KEY `UserId_idx` (`UserId`),
  CONSTRAINT `fk_userid_order` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (43,13,'asdfasdf'),(44,13,'addresstest'),(46,16,'usertestaddressupdatedaddress'),(48,16,'addresstest');
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `products` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `Description` varchar(45) DEFAULT NULL,
  `Price` decimal(10,0) NOT NULL,
  `Image` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (4,'string',NULL,0,NULL),(9,'name','description',30,NULL),(10,'gaming headphones','product description',70,'a1c318b3_71QEWj+ioXS._AC_SX679_.jpg'),(12,'product 1','description product',40,NULL),(13,'product 2','description product',40,NULL),(14,'product 3','description product',40,NULL),(15,'product 4','description product',40,NULL),(16,'product 5','description product',40,NULL);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(45) NOT NULL,
  `FirstName` varchar(45) NOT NULL,
  `LastName` varchar(45) DEFAULT NULL,
  `Email` varchar(45) NOT NULL,
  `Address` varchar(45) NOT NULL,
  `Phone_number` int DEFAULT NULL,
  `Password` varchar(45) NOT NULL,
  `Cartid` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Username_UNIQUE` (`Username`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `Cartid_UNIQUE` (`Cartid`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (13,'stringtest','string','string','string','string',NULL,'RzKH+CmNunFjqJeQiVj3wOrnM+JdLgJ5kuou3JvtL6g=',13),(14,'stringtest2','string','string','string','string',0,'RzKH+CmNunFjqJeQiVj3wOrnM+JdLgJ5kuou3JvtL6g=',NULL),(16,'usertest123','userupdated','testuser','usertest@atest.com','usertestaddressupdatedaddress',777777798,'OtNhk27Z742DXXRa3yTXGRhueE7G/ppSXusARYg9X5E=',3),(17,'usertest12','firstname22','lastname','testemail@usertest.com','addresstest',999333999,'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',12);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-15 17:52:07
