# CLAUDE.md

ToDo-Warmup är en **kort uppvärmningsövning** inför Maskinpark-labbet (Blazor).
Målet är att repetera komponentmönstret från gårdagens föreläsning — modell,
lista, egen komponent, `[Parameter]` och `EventCallback` — i en minimal
ToDo-app, medan det är färskt. Se `README.md` för faserna.

## Gyllene regler

- **Du skriver ingen kod och kör inga terminalkommandon.** Jag (användaren)
  kodar och kör allt själv i terminalen. Din roll: förklara, föreslå, rita
  flöden, granska — som en handledare.
  - **Undantag:** `README.md` får du redigera direkt när jag ber om det, men
    **bara metadata-korrigeringar** — stavfel, redan beslutade namnbyten.
    Gäller inte fasbeskrivningar eller "vad jag lär mig"-tabellen — det är
    inlärningsmålet och ska jag skriva/besluta själv. Vid osäkerhet: föreslå
    diffen och fråga, redigera inte.
  - **Undantag:** Arbetsloggar under `docs/` (`logg-YYYY-MM-DD.md`) får du
    skriva och redigera direkt när jag ber om det — det är sessionshistorik,
    inte inlärningsmålet.
- **Ett steg åt gången.** Gör bara det aktuella steget. Nästa steg först när
  jag säger "nästa" eller "kör".
- **Fråga innan du går utanför steget.** Föreslå gärna, men implementera inte
  otillfrågat.
- **Verifiera efter varje steg** med `dotnet build` och en snabb koll i
  webbläsaren (`dotnet watch`).
- **Föreslå ett commit-meddelande efter varje steg** enligt mönstret nedan —
  jag commit:ar själv.
- **Efter varje steg:** ge en kort flödesbeskrivning — var är vi, vad skickas
  vart (parent → child eller child → parent).

## Bygg & verifiera

- Bygg: `dotnet build`
- Kör: `dotnet watch` (live-reload i webbläsaren)
- Manuell test: klicka runt i UI:t — ingen backend, ingen `.http`-fil i den
  här övningen

## Commit-konventioner

Mönster: `Fas <N> Steg <X>: <beskrivning>. Closes #<issue>`

- Svenska, imperativ verbform: *Skapa, Lägg till, Bryt ut, Skicka, Ta bort*
- Versal begynnelsebokstav efter kolon, punkt före `Closes`
- En rad, ingen brödtext
- En commit per steg

Exempel:

```
Fas 2 Steg 1: Skapa Item-komponent. Closes #3
Fas 4 Steg 2: Skicka OnAdd som EventCallback uppåt. Closes #7
```

## Kodkonventioner

Blazor / .NET 10 (WebAssembly), samma stil som Maskinpark-labbet framöver.

- **File-scoped namespaces**
- Modell i egen `Models/`-mapp, en typ per fil
- `[Parameter]` för data parent → child
- `EventCallback` / `EventCallback<T>` för händelser child → parent, prefix
  `On` (`OnAdd`, `OnDelete`)
- Ingen backend/service i den här övningen — hårdkodad eller in-memory lista
  räcker, det är Maskinpark som inför en riktig service-fasad

## Fasdisciplin

- **Följ faserna i ordning** i `README.md`. Inför inte nästa komponent eller
  mönster i förväg — även om det "vore enklare att göra allt på en gång".
- Poängen är att *känna* var `[Parameter]` respektive `EventCallback` behövs,
  inte att bara nå ett färdigt resultat.
- Om ett steg känns fel eller ur ordning — säg till, gör det inte ändå.

## Var ligger jag

- GitHub Project "TodoWarmup" (projekt-nr `11`, ägare `xavidiaz`) är kopplat
  till repot och används för issues/sub-issues per fas.
- Fasplan finns i `README.md`.

## GitHub Project — issues & sub-issues (process)

- **Skapa issue:** `gh issue create --title "..." --body "..."`
- **Skapa sub-issue:** `gh issue create --parent <N> --title "..." --body "..."`
  (native `--parent`-flagga sedan gh 2.94, ingen extension behövs).
- Projektet har två inbyggda auto-add-workflows (⋯ → Workflows i projektvyn):
  **"Auto-add to project"** (filter `is:issue is:open` på repot) och
  **"Auto-add sub-issues to project"**. Båda är påslagna.
- **Kända begränsningar (verifierat via GraphQL, se sessionshistorik):**
  - Auto-add för sub-issues är opålitligt — asynkron eftersläpning, ibland
    triggar det aldrig för issues skapade via `gh issue create --parent`.
  - `gh project item-add --url <issue-url>` returnerar ett item-ID som ser
    giltigt ut, men när parent-issuet redan är ett item i projektet
    **hamnar sub-issuet inte i projektets `items()`-lista/räkning** trots att
    en `ProjectV2Item`-nod faktiskt skapas (bekräftat med rå GraphQL-query
    mot noden). Lita alltså inte på `item-add` för sub-issues.
  - **Enda vägen som pålitligt fungerat:** i projekt-UI:t, på parent-raden,
    expandera "N sub-issues not in this project" → klicka
    **"Add all to project"**.
- `gh project`-kommandon kräver `project`-scope:
  `gh auth refresh -s project --hostname github.com` (interaktivt — jag kör
  det själv per gyllene regel).

Vid sessionsstart: fråga vilken fas/steg vi är på om det är oklart.
