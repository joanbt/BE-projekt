### Instalacja

```
 $ docker-compose build
 $ docker-compose run php composer install
 $ docker-compose run php sh /var/install/installMagento.sh

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

## Moduły

 - pokaż wszystkie moduły:
	```
	$ docker-compose run php bin/magento module:status
	```
 - włącz moduł:
	```
	$ docker-compose run php bin/magento module:enable <module_name>
	```
 - wyłącz moduł:
	```
	$ docker-compose run php bin/magento module:disable <module_name>
	```

> **Po aktualizacji modułów może być konieczne usunięcie flagi z var/.maintenance.flag**

