# ToDo-Warmup — Uppvärmning inför Maskinpark

En **kort** repetitionsövning i Blazor-komponentmönster, byggd på det som
gicks igenom i gårdagens föreläsning (Blazor Intro / ToDo). Syftet är att
komponentmönstret ska sitta i fingrarna innan Maskinpark-labbet, inte att
bygga en fullständig ToDo-app.

## Pedagogisk idé

Samma princip som MovieApi-Refactor: börja enklast möjliga, känn smärtan,
lägg på nästa lager. Här är smärtan mindre uttalad eftersom det är en
repetition — men samma frågor gäller efter varje fas:

1. **Vad förändras?** — vilket problem löser det här steget?
2. **Vem äger datan nu?** — flyttar vi state uppåt eller nedåt?
3. **Vad skickas vart?** — `[Parameter]` (parent → child) eller
   `EventCallback` (child → parent)?

## Miljö

- .NET 10, Blazor WebAssembly
- LazyVim/Neovim på Omarchy

## Sessionsregler

- **Ett steg åt gången.** Nästa när jag säger "nästa" eller "kör".
- **Verifiering efter varje steg** — `dotnet build` + snabb koll i browsern.
- **Commit efter varje steg** — `Fas N Steg X: <vad>. Closes #<issue>`.
- **Flödesbeskrivning efter varje steg.**

## Faser

### FAS 1 — Modell 🎯

Skapa `TodoItem`-modellen (`Models/TodoItem.cs`): `Id` (Guid), `Text`
(string), `IsDone` (bool).

**Efter fas 1:** en ren datatyp, inget UI ännu.

### FAS 2 — Hårdkodad lista

En sida (`Home.razor` eller liknande) med en hårdkodad `List<TodoItem>` som
renderas med `@foreach` direkt i sidan — ingen egen komponent ännu.

**Problem som uppstår:** allt ligger i en fil, ingen återanvändning.

### FAS 3 — Refaktorering: Item-komponent

**Problem:** rendering av en rad är inbäddad i sidan.
**Lösning:** bryt ut till en `Item`-komponent som tar emot en `TodoItem` via
`[Parameter]`.

### FAS 4 — Refaktorering: Items-komponent

**Problem:** sidan hanterar både listan och loopen.
**Lösning:** en `Items`-komponent som tar emot listan via `[Parameter]` och
loopar `Item` — sidan (`Home`) äger bara listan och skickar den vidare.

### FAS 5 — Input-komponent + lägg till

**Problem:** listan är fortfarande statisk.
**Lösning:** en `Input`-komponent med `@bind` mot ett privat fält och en
knapp. Ny todo skickas uppåt via `EventCallback<string>` (`OnAdd`) till
sidan, som lägger till i sin privata lista.

### FAS 6 — Ta bort

**Problem:** man kan bara lägga till, aldrig ta bort.
**Lösning:** en knapp i `Item` som skickar `EventCallback<Guid>` (`OnDelete`)
uppåt med itemets `Id`, genom `Items` till `Home`, som tar bort ur listan.

### FAS 7 (bonus) — Markera som klar

En checkbox i `Item` som togglar `IsDone`, antingen lokalt eller via ett nytt
`EventCallback<Guid>` (`OnToggle`) uppåt — din bedömning när vi kommer dit.

## Vad jag lär mig efter varje fas

| Efter fas | Jag förstår |
|---|---|
| 1 | Modell = ren datatyp, separat från UI |
| 2 | `@foreach` i Razor, varför en fil snabbt blir rörig |
| 3 | `[Parameter]` — data parent → child |
| 4 | Ansvarsuppdelning: vem äger listan, vem loopar |
| 5 | `EventCallback<T>` — data/händelse child → parent |
| 6 | Samma mönster igen, men med `Guid` som identifierare |
| 7 (bonus) | Toggle-state — lokalt vs uppåt i trädet |

## Vad detta INTE innehåller (medvetet)

- ❌ Backend/API — kommer i Maskinpark-labbet
- ❌ Persistens (databas) — inte relevant för en 20-minuters uppvärmning
- ❌ Validering, felhantering — kommer senare övningar

Fokus här: **`[Parameter]` och `EventCallback` ska sitta**, inget annat.
