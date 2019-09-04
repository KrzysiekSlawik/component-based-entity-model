# component-based-entity-model
Zbiór komponentów opisujących entity do gry 2d topdown, którą tworzę. 
* [Brain](https://github.com/KrzysiekSlawik/component-based-entity-model/blob/master/README.md#brain)
* [Animator](https://github.com/KrzysiekSlawik/component-based-entity-model/blob/master/README.md#spritesheetanimator)
* [Inne](https://github.com/KrzysiekSlawik/component-based-entity-model/blob/master/README.md#others)
## Brain
Brain to komponent odpowiedzialny za AI każdego entity, które potrzebuje AI. Zbudowane zostało w taki sposób, by edycja z poziomu edytora Unity była możliwa, wygodna nawet w standardowym inspektorze i otwarta na rozszerzenia. Główna część komponentu oparta jest o wzorzec projektowy FSM. Rozdzielenie warstwy stanów i przejść pozwala na częstsze wykorzystywanie już napisanego kodu, a nawet tworzenie całkiem nowych stanów nie dotykając kodu.
Metody:
* **Start()** inicjalizacja komponentu i wszystkich jego składowych, wywoływana automatycznie(przy tworzeniu komponentu)
* **Update()** aktualizacja komponentu i komunikacja z jego składowymi, wywoływana automatycznie(raz na klatkę symulacji)
* **PlayerPosition()** zwraca pozycję najbliższego z graczy
* planowane rozszerzenie o dodatkowe zapytania o otoczenie
![alt text](https://github.com/KrzysiekSlawik/component-based-entity-model/blob/master/brain.png "widok w inspektorze")
### BBehaviour
Abstrakcyjna klasa odpowiedzialna za sterowanie entity. Obiekty tej klasy stanowią komponenty dla BState i zawierają referencję do Brain, dzięki czemu przepływ informacji jest obustronny. Klasa BBehaviour dziedziczy po UnityEngine.ScriptableObject dzięki czemu można tworzyć i modyfikować serializowane obiekty już z poziomu edytora.
Metody:
* **LoadBrain(Brain brain)** przyjęcie referencji obiektu Brain
* **abstract Update()** aktualizacja komponentu i komunikacja z komponentem Brain, wywoływana automatycznie (raz na klatkę symulacji)

### BSTransitions
Abstrakcyjna klasa odpowiedzialna za przedstawianie warunków przejścia. Obiekty tej klasy stanowią pośredni komponent klasy BState. Klasa BSTransition dziedziczy po UnityEngine.ScriptableObject dzięki czemu można tworzyć i modyfikować serializowane obiekty już z poziomu edytora.
Metody:
* **Link(Brain brain)** przyjęcie referencji obiektu Brain
* **abstract Eval()** odpowiada czy powinno dojść do przejścia 
### BPair
Kolekcja zawierająca referencję na BSTransition i nazwę stanu, do którego ma prowadzić przejście. Ułatwia uporządkowanie i czytelną prezentację w inspektorze edytora Unity.
### BState
Klasa kumulująca informacje na temat stanu:
* zachowanie (BBehaviour)
* listę wszystkich przejść (warunków BSTransition i nazw stanów)
* własną nazwę, która służy jako klucz w słowniku wszystkich stanów danego Brain

Klasa ta jest wpełni serializowalna i edytowalna z poziomu inspektora Unity. W miarę rozrastania się bazy zachowań (BBehaviour) i warunków przejść (BSTransition) pozwoli na tworzenie całkowicie nowych kompozycji bez ingenercji w kod źródłowy.

Metody:
* **EvalTransitions()** wywołuje Eval() na kolejnych przejściach aż do pozytywnej odpowiedzi i zwraca string z nazwą stanu, do którego prowadzi przejście dodatnio ewaluowane lub własną nazwę stanu w przypadku braku pozytywnej ewaluacji.
## Others
Folder z wszystkimi pomniejszymi komponentami, które odpowiadają za podstawowe funkcje entity. 
### Attributes
Klasa przedstawiająca atrybuty entity wykorzystywane przez pozostałe komponenty. Oczekuje na przebudowe, gdyż nie jest dość elastyczna, by być rozbudowany do utrzymania prawdziwego systemu statystyk potrzebnego w grze rpg.
### EntityMovement
Klasa odpowiedzialna za ruch entity. Metody:
* **Start()** inicjalizacja komponentu, wywołana automatycznie (przy stworzeniu komponentu)
* **Update()** aktualizacja komponentu i komunikacja z zależnymi komponentami (wywołana automatycznie raz na klatkę symulacji)
* **GoTo(Vector2 target)** ustanowienie celu i aktualizacja pól odpowiedzialnych za nawigację bez pathfindingu
* **GoToWithPF(Vector2 taget)** ustanowienie celu, złożenie zapytania o trasę do systemu PathFinding'u i aktulizacja pól odpowiedzialnych za nawigację
* **UpdateMoveVec(Vector2 vec)** aktualizacja wektora ruchu, wykorzystywana przez komponenty takie jak brain czy PlayerControler
### Facing
Klasa odpowiedzialna za ustalanie i aktualizowanie kierunku skierowania postaci. Metody:
* **Start()** pobranie referencji na komponenty UnityEditor.Rigidbody2D
* **Update()** aktualizacja pola face
* **GetFace()** zwraca aktualny skierowanie postaci w postaci 8 kierunkowej (enum)
### PlayerControler
Klasa odpowiedzialna za sterowanie przez gracza entity. Została stworzona jedynie do testowania pozostałych komponentów i będzie potrzebowała gruntownej przebudowy.
## SpriteSheetAnimator
Komponent odpowiedzialny za animowanie entity. System dostarczany przez Unity jest mocno ograniczony w przypadku animacji poklatkowej, którą chcę wykorzystać w swojej grze. Oddzielenie warstwy animacji od spriteSheet'u, z którego powstaje pozwala na podmienienie spriteSheetu bez konieczności aktualizowania animacji (co jest niemożliwe w Unity). Komponent na zewnątrz udostępnia jedynie metodę **SetAnimation(SpriteSheetAnimation animation)** służącą do zmiany animacji.
### AnimatorController
Komponent odpowiedzialny za zarządzanie animacjami entity. Oparty o wzorzec projektowy FSM pozwala na niemalże w pełni bez kodowe tworzenie nowych zachowań. Klasa została tak zaprojektowana, by większość pracy wykonywanej z jej pomocą odbywała się w inspektorze edytora Unity, tworzenie nowych stanów jak i warunków przejścia odbywa się w inspektorze, użytkownik musi jednak napisać skrypt odpowiedzialny za aktualizowanie zmiennych kontrolera.
Metody:
* **SetFloat(string key, float value)** aktualizowanie zmiennych typu float ustalonych w inspektorze
* **SetBool(string key, bool value)** aktualizowanie zmiennych typu bool ustalonych w inspektorze
* **GetFloat(string key)** getter dla floatów ustalonych w inspektorze
* **GetBool(string key)** getter dla booli ustalonych w inspektorze
* **TransitionTo(string stateName)** przejdź do stanu o danej nazwie
![alt text](https://github.com/KrzysiekSlawik/component-based-entity-model/blob/master/animcontroller.png "widok w inspektorze")
### SSAState
Obiekt w pełni serializowalny odpowiedzialny za przedstawianie stanu animatora. Zawiera wszystkie przejścia i animację dotyczącą danego stanu.
* **Inject(AnimatorController controller)** wykorzystywany do przypisania referencji kontrolera
#### BoolCondition
Obiekt prezentujący warunek oparty o zmienną boolowską. Zawiera metodę **Eval(AnimatorController controller)** wykorzystywaną przez SSAState w metodzie **Update()**.
#### FloatCondition
Obiekt prezentujący warunek oparty o zmienną typu float. Zawiera metodę **Eval(AnimatorController controller)** wykorzystywaną przez SSAState w metodzie **Update()**.
#### Transition
Obiekt przedstawiający wszystkie dane przejścia:
* dowolną liczbę BoolCondition
* dowolną liczbę FloatCondition
* nazwa stanu, do którego prowadzi przejście

Oraz metodę **Eval()**, wywołuje metody Eval() na wszystkich BoolCondition i FloatCondition należących do Transition.
## PathFinding
W fazie projektowej i doszkalania się - w chwili obecnej częściowo działa, jednak przez ilość błędów nie jest wykorzytywany, dlatego też nie jest opisany w tej dokumentacji.
