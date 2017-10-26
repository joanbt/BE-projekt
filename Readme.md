### Run

```
 $ docker-compose up
```

### Store Address
	
	http://127.0.0.1:8880/

### Magento Admin Address

	http://127.0.0.1:8880/admin_aro8a3/

### phpMyAdmin

	http://127.0.0.1:8080/

> All login credentials are in .env file

## Modules

 - show all modules:
	```
	$ docker-compose run php bin/magento module:status
	```
 - enable module:
	```
	$ docker-compose run php bin/magento module:enable <module_name>
	```
 - disable module:
	```
	$ docker-compose run php bin/magento module:disable <module_name>
	```

> **After module update it may be nessecary to remove maintance flag from var/.maintenance.flag**

