<h1 class="code-line" data-line-start=0 data-line-end=1 ><a id="Tbz_Modul_254_PdfPrinter_0"></a>Tbz Modul 254 PdfPrinter</h1>
<h3 class="code-line" data-line-start=2 data-line-end=3 ><a id="Notwendige_Applikationen_2"></a>Notwendige Applikationen:</h3>
<p class="has-line-data" data-line-start="3" data-line-end="4">Postman</p>
<h3 class="code-line" data-line-start=5 data-line-end=6 ><a id="Anleitung_5"></a>Anleitung</h3>
<p class="has-line-data" data-line-start="6" data-line-end="7">Run exe:</p>
<ol>
<li class="has-line-data" data-line-start="7" data-line-end="8">Pulendran-Widerkehr-M254.zip und TBZ-254.postman_collection.json heruterladen.</li>
<li class="has-line-data" data-line-start="8" data-line-end="9">Postman öffnen und über File &gt; Import das TBZ-254.postman_collection.json importieren.</li>
<li class="has-line-data" data-line-start="9" data-line-end="10">Zip entpacken (Dateistruktur nicht ändern!).</li>
<li class="has-line-data" data-line-start="10" data-line-end="11">TBZ.Modul.254.PrintApi.exe ausführen.</li>
<li class="has-line-data" data-line-start="11" data-line-end="12">Über Postman GET befehl “Get Printer and Printer Tray” ausführen um alle Drucker und Druckerschubladen als json zu erhalten.</li>
<li class="has-line-data" data-line-start="12" data-line-end="13">POST “Generate PDF and Print it automatically” ausführen um PDF zu generieren und es automatisch auszudrucken.</li>
</ol>
<p class="has-line-data" data-line-start="16" data-line-end="19">### API<br>
URL für GET: <a href="http://localhost:5000/printer">http://localhost:5000/printer</a><br>
URL für POST: <a href="http://localhost:5000/pdf">http://localhost:5000/pdf</a></p>
<p class="has-line-data" data-line-start="20" data-line-end="38">PrinterName | String: Aus Get Printer “PrinterName”<br>
PrinterTray | String: AUs Get Printer “PrinterTray”<br>
IsLandscape | Boolean: Jenach Druckschacht false oder ture wenn papier Quer eingelegt ist.<br>
PaperSize   | String: Zurzeit nur auf A4 möglich.<br>
IsColor     | Boolean: Um frabig zu drucken ture, ansonst false<br>
Sex         | String: Geschlecht<br>
Firstname   | String: Vorname<br>
Lasrtname   | String: Nachname<br>
Street      | String: Strasse<br>
StreetNr    | String: Hausnummer<br>
City        | String: Stadt<br>
ZipCode     | String: PLZ<br>
Country     | String: Land<br>
PhoneNumber | String: Telefonnummer<br>
BirthdayDate| String: Geb. Datum<br>
Occupation  | String: Berufsbezeichnung<br>
Description | String: Bemerkung<br>
Photo       | String: Passfoto als Base64 übergeben. =&gt; <a href="https://base64.guru/converter/encode/image">Hier können Sie das Bild in Base64 umwandel</a></p>
