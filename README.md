Da bi se pokrenula aplikacija potrebno je ispratiti sledeće korake:
1.	Pokrenuti Init.sh shell skriptu koja kreira instancu baze, primenjuje migracije i popunjava bazu podacima
2.	Instalirati npm pakete u okviru web aplikacije komandom:
npm install
3.	Pokrenuti api komandom:
dotnet run --urls=https://localhost:7194/
4.	Pokrenuti web aplikaciju komandom:
npm run start

Da bi se aplikacija koristila neophodno je biti ulogovan, odnosno u local storage mora postojati access token zabeležen u formatu:
Key: token
Value: eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjI3NDk4YTZhLTlmNzktNDAwNC0yYjIzLTA4ZGM0YTkxZTQwYSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Im1pbGFuc3RvamtvdmljMjcwN0BnbWFpbC5jb20iLCJleHAiOjE3MTE4MzQ3MjN9.ky5GDXKKzwov1ImjH5A_nUD0gCk2CuoYAfB828IQAteRUqqjnwQCE0bEr0L1N3D1xmyEnJznwyTsuerJtYwlDw

Token je potrebno pribaviti pozivom Signup metode ili SignIn metode sa nekim od sledećih naloga:
{
  "email": "test@fazi.rs",
  "password": "fazi"
},
{
  "email": "milanstojkovic2707@gmail.com",
  "password": "password"
}


