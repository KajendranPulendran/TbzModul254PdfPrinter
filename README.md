# Tbz Modul 254 PdfPrinter

### Notwendige Applikationen:
Postman

### Anleitung
Run exe:
1. Pulendran-Widerkehr-M254.zip und TBZ-254.postman_collection.json heruterladen.
2. Postman öffnen und über File > Import das TBZ-254.postman_collection.json importieren.
3. Zip entpacken (Dateistruktur nicht ändern!).
4. TBZ.Modul.254.PrintApi.exe ausführen.
5. Über Postman GET befehl "Get Printer and Printer Tray" ausführen um alle Drucker und Druckerschubladen als json zu erhalten. 
6. POST "Generate PDF and Print it automatically" ausführen um PDF zu generieren und es automatisch auszudrucken. 



###API
URL für GET: http://localhost:5000/printer
URL für POST: http://localhost:5000/pdf

PrinterName | String: Aus Get Printer "PrinterName"
PrinterTray | String: AUs Get Printer "PrinterTray"
IsLandscape | Boolean: Jenach Druckschacht false oder ture wenn papier Quer eingelegt ist.
PaperSize   | String: Zurzeit nur auf A4 möglich. 
IsColor     | Boolean: Um frabig zu drucken ture, ansonst false
Sex         | String: Geschlecht
Firstname   | String: Vorname
Lasrtname   | String: Nachname
Street      | String: Strasse
StreetNr    | String: Hausnummer
City        | String: Stadt
ZipCode     | String: PLZ
Country     | String: Land
PhoneNumber | String: Telefonnummer
BirthdayDate| String: Geb. Datum
Occupation  | String: Berufsbezeichnung
Description | String: Bemerkung
Photo       | String: Passfoto als Base64 übergeben. => [Hier können Sie das Bild in Base64 umwandel](https://base64.guru/converter/encode/image)
