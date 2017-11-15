### Instalacja

```
 $ docker-compose build
 $ docker-compose run php composer install

```

### Run

```
 $ docker-compose up
```

### adres sklepu
	
	http://127.0.0.1:8880/

### Magento Admin

	http://127.0.0.1:8880/admin/

### phpMyAdmin

	http://127.0.0.1:8080/

> Wszystkie dane do logowania znajdują się w pliku .env


### parser 
```
Uruchomcie /Ikea_parser/Ikea_parser.sln i rozpakujcie DATA.zip.
W projekcie musicie zmienic PATHTOFILE na sciezke do rozpakowanego DATA.zip
```

### Dump bazy danych

```
 $ docker-compose exec mysql bash
 $ mysqldump -uroot -p${DATABASE_ROOT_PASSWORD} --databases ${DATABASE_NAME} > docker-entrypoint-initdb.d/2_db.sql
```

##### Aby wczytać bazę danych należy:

 - usunąć kontener mysql:
```
 $ docker rm beprojekt_mysql_1
```
 - ponownie uruchomić projekt:
```
 $ docker-compose up
```
