# component-based-entity-model
unity project
## Brain
Brain to komponent odpowiedzialny za AI każdego entity, które potrzebuje AI. Zbudowane zostało w taki sposób, by edycja z poziomu edytora Unity była możliwa, wygodna nawet w standardowym inspektorze i otwarta na rozszerzenia. Główna część komponentu oparta jest o wzorzec projektowy FSM. Rozdzielenie warstwy stanów i przejść pozwala na częstsze wykorzystywanie już napisanego kodu, a nawet tworzenie całkiem nowych stanów nie dotykając kodu.
### BBehaviour
### BSTransitions
### BPair
### BState
## Others
### Attributes
### EntityMovement
### Facing
### PlayerControler
## SpriteSheetAnimator
### AnimatorController
### SSAState
#### BoolCondition
#### FloatCondition
#### Transition
#### Body
## PathFinding
W fazie projektowej i doszkalania się - w chwili obecnej częściowo działa, jednak przez ilość błędów nie jest wykorzytywany, dlatego też nie jest opisany w tej dokumentacji.
