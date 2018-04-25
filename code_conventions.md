# Code Conventions

Mostly based on idsoftware's Doom3 code conventions:
* http://kotaku.com/5975610/the-exceptional-beauty-of-doom-3s-source-code

Other good reads on healthy coding habits:
* https://mollyrocket.com/casey/stream_0019.html

## General principles

* "I hate code and i want as little of it as possible in our product" - Jack Diederich
Don't write code. Write new code only when everything else fails.

* No inheritance and virtual functions. Always use composition.

* Don't stray from left margin too far or too long

* Code should be locally coherent and single-functioned: one function should do exactly one thing. 
It should be clear about what it’s doing.
Local code should explain, or at least hint at the overall system design.

* Code should be self-documenting. 
Comments should be avoided whenever possible. 
Comments duplicate work when both writing and reading code. 
If you need to comment something to make it understandable it should probably be rewritten.
(Good read: http://kotaku.com/5975610/the-exceptional-beauty-of-doom-3s-source-code)

* All variables/properties are public by default, private if no class outside touches it.
Do not ever use getters/setters. They are harder to read, more lines of code, add cruft/complexity, CPU overhead.

* Prefer commenting sections over extracting them.
Whenever you have a function, it implicitly says it can be called from anywhere inside the class.
Never have a function do something that happens in only one place.

* Make everything you as explicit as possible. Cleaner for all programmers and the compiler.

* Constrain scope of local variables
Don't leak out data where it doesn't need to be.
Don't keep data around longer than it needs to be.

* Filenames always lowercase and words divided with an underscore _ (because of compatibility)


## Syntax

* Use tabs that equal 4 spaces.

* Use typically trailing braces everywhere (if, else, functions, structs, class definitions, etc.)
```csharp
if ( x ) {
}
```

* The else statement starts on the same line as the last closing brace.
```csharp
if ( x ) {
} else {
}
```

* Pad parenthesized expressions with spaces
```csharp
// Like this:
if ( x ) {
}

// Not like this:
if (x) {
}
```

And
```csharp
// Like this:
x = ( y * 0.5f );

// Not like this:
x = (y * 0.5f);
```

* Use precision specification for `floating point` values unless there is an explicit need for a `double`.
```csharp
// Like this:
float f = 0.5f;

// Not like this:
float f = 0.5;
```

* Function names start with an upper case:
```csharp
void Function( void );
```

* In multi-word function names each word starts with an upper case:
```csharp
void ThisFunctionDoesSomething( int SomeValue );
```

* Variable names start with a lower case character.
```c
float x;
```

* In multi-word variable names the first word starts with a lower case character and each successive word starts with an upper case.
```c
float maxDistanceFromPlane;
```

* Struct names use the same naming convention as variables, however they always end with "_t".
```c
struct renderEntity_t;
```

* Private instance properties start with "m_".
```csharp
class Game
{
    public  int     radius     = 5; // suggestion for editor
    private Vector3 m_ballPos;
    
    void Start() 
    {
	m_ballPos = transform.position;
    }
}
```

* Enum names use the same naming convention as variables, however they always end with  "_t". 
The enum constants use all upper case characters. Multiple words are separated with an underscore.

```c
enum contact_t {
	CONTACT_NONE,
	CONTACT_EDGE,
	CONTACT_MODELVERTEX,
	CONTACT_TRMVERTEX
};
```

* Names of recursive functions end with "_r"

```csharp
void WalkBSP_r( int node );
```

Constants use all upper case characters. Multiple words are separated with an underscore.
All constants can be gathered in a `static class` called `Constants`.

```csharp
static class Constants {
    public const float PI32 = 3.14159;
    public const int SPEED_OF_LIGHT = 300000; // km per sec.
}
class Game {
    void Start() {
        float radius = 5.3;
        float area = Constants.PI32 * (radius * radius);
        int secsFromSun = 149476000 / Constants.SPEED_OF_LIGHT; // in km
    }
}
```

* Indent the names of class variables to make nice columns.

```csharp
class Foo {
    float      x;
    float      y;
    float      z;
    Rigidbody  rb;
```

* Ording of class variables and methods should be as follows:

- list of friend classes
- public variables
- public methods
- protected variables
- protected methods
- private variables
- private methods

This allows the public interface to be easily found at the beginning of the class.


* Explicitly named functions tend to be less prone to programmer error and inadvertent calls to functions due to wrong data types being passed in as arguments.  
```csharp
Anim = GetAnim(0);
// This could be meant as a call to get a random animation, but the compiler would interpret it as a call to get one by index.
```
Better:
```csharp
Anim = GetAnimByIndex(0);
```




## On Game Design 

### Include experts in all fields
"Finally, it cannot be overstated how important the Architects were in the process. For the Glass Factory in particular, they provided very precise assembly details for the wood structure, weight distribution, roof design , doors, etc. It would have been impossible, even with a lot of research on our side, to arrive at this level of detail. Looking back at all my previous projects, I cringe at how bad the architecture was. And the same applies to costume and character design. Game developers should always try when possible to consult with specialists in the areas they are trying to work in! "
- Luis Antonio, Artist on The Witness
http://www.artofluis.com/3d-work/the-art-of-the-witness/soon-symmetry-island-and-glass-factory/


# Examples

### Scoping

Scope your variables as tight as possible.
If variables go out of scope they are freed in memory.

A scope is denoted by curly braces:

```csharp
// GLOBAL SCOPE HERE
{
    // SCOPE 1
    int a = 1;
    
    {
        // SCOPE 1a
        // you have access to variable 'a'
        int b = 2;
        // you have access to 'a' and 'b' now.
    }

    // b is gone now. Only 'a' is known here.

    {
        // SCOPE 1b
        // you have access to variable 'a'
        int c = 3;
        // you have access to 'a' and 'c'
    }
    // c is gone now. Only 'a' is known here.
} // end of SCOPE 1
// GLOBAL SCOPE HERE
```

This is a good example: you grab the attached `Rigidbody` component in a class variable at `Start`.
From then on you can refer to it in each function without having to grab it each time.

```csharp
Rigidbody rb;

void Start() {
    rb = GetComponent<Rigidbody>();
}

void Update() {
    // You can now do something with rb
}
```

Don't grab GetComponent<Rigidbody>() (calling into Unity engine) each tick!
You want to have the same object each time, so it should be scoped outside.

```csharp
void Update() {
    // ugh, don't grab it each tick :(
    Rigidbody rb = GetComponent<Rigidbody>();   
}
```

