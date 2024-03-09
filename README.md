# ICS projekt 

## Cíl
Cílem je vytvořit použitelnou a snadno rozšiřitelnou aplikaci, která splňuje požadavky zadání. Aplikace nesmí padat nebo zamrzávat. Pokud uživatel vyplní něco špatně, je upozorněn validační hláškou.

<!-- Project specific -->
# Téma projektu
Tématem letošního projektu bude vytvoření aplikace umožňující jejím uživatelům správu aktivit a měření času stráveného danou aktivitou - Toggl Track, Kimai, atd. 

---

<!-- Project specific -->
## Data
V rámci dat, se kterými se bude pracovat budeme požadovat minimálně následující data.

### Uživatel
- Jméno
- Příjmení
- Fotografie (postačí url)
- (Aktivity)
- (Projekty)

### Aktivita
- Začátek (datum, čas)
- Konec (datum, čas)
- Typ / tag aktivity (postačí enum, nebo uživatelem definovaná hodnota)
- Popis aktivity
  
### Projekt
- Název
- (Aktivity)
- (Uživatelé)

> () anotují vazby mezi entitami

---
## Základní funkcionalita
Aplikace bude obsahovat několik pohledů pro zobrazování přehledu, zobrazení detailu a vložení dat. 


Pro uložení zvolte [SQL Server Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb), která je nainstalována jako součást Visual Studio - Data storage and processing workloadu. Alternativně můžete také využít **SQLite**. Jako ORM framework použijte [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/).

<!-- Project specific -->
*Minimální* funkcionalita:
  - **Aplikace musí umožnit provést CRUD operace nad všemi daty.**
  - **Aplikace se ovládá z pohledu vybraného uživatele při spuštění aplikace.**
  - Uživatel může vytvořit jiné uživatele.
  - Uživatel může upravit informace o sobě.
  - Uživatel může přidat záznam o aktivitě (bude u ní uveden jako osoba provádějící aktivitu).
  - Uživatel vidí seznam projektů a může se přihlásit do projektu.
  - Uživatel může **filtrovat** aktivity podle začátku a konce.
  - Uživatel může **filtrovat** aktivity uživatelsky přívětivě bez zadávání datumu za poslední týden, měsíc, předcházející měsíc a rok.
  - Uživatel může vykonávat pouze jednu aktivitu v jeden čas. Tedy, zaznamenané aktivity se nesmí překrývat.


---
## Architektura projektu
Architektura aplikace je jeden z důležitých stavebních kamenů při vývoji SW. V rámci cvičení se seznámíte s vrstvenou architekturou demonstrující logickou separaci tříd do projektů (alespoň App, BL, DAL), kterou vřele doporučujeme využít i ve Vašich projektech (klidně 1:1). 

---
## Správa projektu - Azure DevOps
Při řešení projektu využijte Azure DevOps a GIT na sdílení kódu. 

---
# Odevzdávání
Odevzdávání projektu má **3 fáze**. V každé fázi se hodnotí jiné vlastnosti projektu. Nicméně, fáze na sebe navazují a v následující fázi pokračujete v práci na svém kódu.

 
---
### Fáze 1 – objektový návrh, databáze 
V téhle fázi se zaměříme na *datový návrh*. Vyžaduje se po Vás, aby datový návrh splňoval zadání a nevynechal žádnou podstatnou část. Zamyslete se nad vazbami mezi jednotlivými entitami v datovém modelu. V této fázi budeme chtít, abyste **odevzdali kód**, kde budete mít *entitní třídy*, které budou obsahovat všechny vlastnosti, které budete dále potřebovat a vazby mezi třídami. 

Abyste si vazby dokázali představit, vytvořte již v tuto chvíli DAL projekt obsahující `DbContext` s `DbSet`y Vašich entitních tříd. Přiložte **ER diagram** vygenerovaný z kódu např. doplňkem [EF Core Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools), nebo generovaným v [Rider/Datagrip](https://www.jetbrains.com/help/rider/Creating_diagrams.html). 

> :warning: Ručně vytvořený ER diagram, který neodpovídá Vašemu kódu je neakceptovatelný.

Pro zajištění vzájemného pochopení všemi členy týmu budeme nově také požadovat vytvoření **wirefame** na všechny pohledy (opět libovolný nástroj či ručně kreslené), které ve vaší výsledné aplikaci chcete implementovat. Tyto wireframy nebudou závazné, ale umožní Vám ihned na začátku vzájemně komunikovat představy o výsledné podobě aplikace. TIP: Při tvorbě wireframe zjistíte, jaká data budete potřebovat a navrhnete korektně nejen vazby v Entitní vrstvě, ale také Modely BL vrstvy, jejichž rozmyšlení jistě oceníte v druhém odevzdání.

ER diagram a wireframy umístěte do kořene repositáře do adresáře **docs**. Formát souborů zvolte tak, aby se daly otevřít rozumným způsobem bez nutnosti instalace specifických nástrojů přímo v prostředí Azure DevOps. Ideální je obrázek ve formátu png, jpeg, svg, pdf...

Hodnotíme:
-   logický návrh tříd
-   využití abstrakce, zapouzdření, polymorfismu - kde to bude dávat smysl a eliminuje duplicity
-   verzování v GITu po logických částech
-   logické rozšíření datového návrhu nad rámec zadání (bonusové body) - toto rozšíření ovšem zvažte; často se stává, že si tím založíte na spoustu komplikací v pozdějších fázích; body za rozšíření dostanete až u obhajoby, pokud je naimplementujete kompletně do výsledné aplikace
-   generovaný ER diagram (logickou strukturu)
-   Wireframy (logickou strukturu, uživatelskou přívětivost, ne kvalitu grafického zpracování)
-   využití **Entity Framework Core - Code First** přístupu na vytvoření databáze z entitních tříd
-   existenci databázových migrací (alespoň InitialMigration)

---
### Fáze 2 – repositáře a mapování
Vytvořte napojení modelů/DTO tříd pomocí Entity Frameworku na databázi. 

Vytvořte tedy repositářovou (Repository) vrstvu, která zapouzdří databázové entity a Fasádu, která zpřístupní pouze data přemapovaná do modelů/DTO. **Inspirujte se ve cvičeních anebo vytvořte vlastní infrastrukturu**.

Protože nemáte zatím UI, funkčnost aplikace ověřte automatizovanými testy! Kde to dává logický smysl tvořte **UnitTesty**, pro propojení s databází vytvářejte **Integrační testy**. Doporučujeme použití testovacího frameworku **xUnit**.

Dbejte kvality Vašeho kódu! Opravte si kód odevzdaný v předchozí fázi dle doporučení v review a zásad Clean Code / SOLID, které dále důsledně dodržujte. Můžete si dopomoct rozšířeními a analyzátory kódu.

Hodnotíme:
- opravení chyb a zapracování připomínek, které jsme vám dali v rámci hodnocení fáze 1
- návrh a funkčnost repositářů
- návrh a funkčnost fasád
- čistotu kódu
- pokrytí aplikace testy - ukážete tím, že repositáře opravdu fungují
- dejte pozor na zapouzdření databázových entit pod vrstvou fasád, která je nepropaguje výše, ale přemapovává na modely/DTO
- funkční build v Azure DevOps
- výsledek testů v Azure DevOps po buildu

---
### Fáze 3 – MAUI frontend, data binding
V této fázi se od Vás již požaduje vytvoření MAUI aplikace. 

Napište backend aplikace (ViewModely), který napojíte na Vámi navržené datové modely z 2. fáze, které jsou zapouzdřeny za vrstvou fasád. 

A dále frontend (View), který bude zobrazovat data předpřipravená ve ViewModelech. Zamyslete se nad tím, jakým způsobem je vhodné jednotlivá data zobrazovat.

> :warning: **Použití aplikace by mělo být intuitivní.**

Využijte *binding* v XAML kódu (vyvarujte se code-behind). Účelem není jenom udělat aplikaci, která funguje, ale také aplikaci, která je správně navržena a může být dále jednoduše upravitelná a rozšířitelná. Dbejte tedy zásad probíraných ve cvičeních.

Za aplikace, jejichž vizuální návrh bude proveden dobře, a zároveň budou plně funkční, budeme udělovat bonusové body.

Hodnotíme:
- opravení chyb a zapracování připomínek, které jsme vám dali v rámci hodnocení fází 1 a 2
- funkčnost celé výsledné aplikace
- vytvoření View, ViewModelů
- zobrazení jednotlivých informací dle zadání – seznam, detail…
- správné využití data-bindingu v XAML
- čistotu kódu
- validaci vstupů

Doporučujeme (bonusové body):
- pokrytí ViewModelů testy
- vytvoření dobře vypadající a plně funkční aplikace
- plánování projektu (logická struktura rozložení práce)

---
## Obhajoba

Na obhajobu se dostaví **celý tým**. Z členů týmu bude vybrán jeden, který obhajobu povede. Na obhajobu nevytvářejte žádnou prezentaci! Budete nám muset ukázat, jak funguje váš kód, a že je správně navržen. Připravte se na naše otázky k funkcionalitě jednotlivých tříd a k důvodům jejich členění. Na obhajobu bude mít tým 10-15 minut.
