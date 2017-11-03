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

### Uzycie myTheme
Sprawdzamy czy istnieje  /web/app/design/frontend/Magento/theme-frontend-my .Jesli nie istinieje - musimy skopiowac ten folder (w przypadku potrzeby zmieniamy uprawnienia uzywac sudo chmod) .
Wchodzimy do panelu admistratora stores->configuration->design i zmieniamy "luma" na "my".Zapisujemy zmiany.
Potem system->cache managment->flush cache. Sprawdzamy glowna strone.

