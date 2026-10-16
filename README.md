# Kolm rakendust (Ülesanne 2)

## Projekti nimi
**KolmRakendust** – Avavorm + kolm rakendust

## Eesmärk ja kirjeldus
Harjutus koosneb **avavormist** ja **kolmest rakendusest**, mis avanevad
avavormi nuppude kaudu. Kogu kasutajaliides on loodud **ainult koodiga**
(Toolbox'i ei kasutatud) ja iga vorm on eraldi klass (**OOP printsiip**).

| Fail | Kirjeldus |
| --- | --- |
| `Avavorm.cs` | Avavorm kolme nupuga – iga nupp avab ühe rakenduse |
| `PictureViewerForm.cs` | Pildi vaatamise programm (OpenFileDialog, ColorDialog, Stretch) |
| `MathQuizForm.cs` | Matemaatiline äraarvamismäng nelja tehtega (+, −, ×, ÷) ja taimeriga |
| `MatchingGameForm.cs` | Sarnaste piltide leidmise mäng 4×4 väljakul (Webdings ikoonid) |

## Kasutatud tehnoloogiad
- C#
- Windows Forms (WinForms)
- .NET 8 (töötab ka .NET Framework 4.7.2+ projektis)

## Kuidas käivitada ja kasutada

### Variant A – uus projekt Visual Studios
1. Ava Visual Studio → **Create a new project** → **Windows Forms App** (nimeks `KolmRakendust`).
2. Kopeeri kõik `.cs` failid kaustast `Projekt2_KolmRakendust` oma projekti kausta
   (asenda olemasolev `Program.cs`, tühi `Form1.cs` võib kustutada).
3. Käivita **F5** – avaneb avavorm, kus saab valida ühe kolmest rakendusest.

### Variant B – olemasolev kaust
Kui masinas on .NET 8 SDK, piisab kaustas käsust:
```
dotnet run
```

### Kasutamine
- **Pildi vaatamise programm** – "Show a picture" avab pildi, "Stretch" venitab
  selle aknale, "Set the background color" muudab taustavärvi.
- **Matemaatiline äraarvamismäng** – "Start the quiz" käivitab 30 sekundit;
  sisesta vastused kõikidesse ridadesse enne aja lõppu.
- **Sarnaste piltide leidmise mäng** – klõpsa kahte ruutu ja leia kõik
  8 ikoonipaari.

## Arendusideed

### Pildi vaatamise programm
1. Piltide slaidishow automaatse vahetusega (Timer + Start/Stop nupp).
2. Pildi salvestamine teise formaati nupu abil (SaveFileDialog).
3. Nupud "Eelmine pilt" / "Järgmine pilt" kausta piltide sirvimiseks.

### Matemaatiline äraarvamismäng
1. Raskusastme valik (kerge 1–10, keskmine 1–50, raske 1–100).
2. Punktiarvestus ja tulemuste salvestamine faili.
3. Juhuslikud tehtemärgid igal voorul.

### Sarnaste piltide leidmise mäng
1. Tegelikud pildid sümbolite asemel (nt pildid kaustast `Images`).
2. Taimer ja punktisüsteem (klõpsude loendamine).
3. Tasemed: 4×4, 6×6 ja 8×8 väljakud.

## Eeldav edasiareng
Avavormist saab arendada väikese "mängude keskuse", kuhu lisanduvad uued
rakendused eraldi klassidena. Iga mäng võib saada oma seadete dialoogi,
parimate tulemuste tabeli ja heliefektid.
