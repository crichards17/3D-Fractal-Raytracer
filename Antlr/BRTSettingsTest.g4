grammar BRTSettingsTest;

file
    : setting+ EOF
    ;

setting
    : key COLON value SEMICOLON
    ;

key
    : STRING
    ;

value
    : primitive
    | array
    | vector
    ;

primitive
    : INT
    | DOUBLE
    | BOOL
    | STRING
    ;

array
    : '[' list ']'
    ;

vector
    : '<' list '>'
    ;

list
    : value (',' value)*
    ;

COLON
    : ':'
    ;

SEMICOLON
    : ';'
    ;

INT
    : [1-9][0-9]*
    ;

DOUBLE
    : INT '.' [0-9]*
    ;

BOOL
    : [Tt] [Rr] [Uu] [Ee]
    | [Ff] [Aa] [Ll] [Ss] [Ee]
    ;

STRING
    : [.]+
    ;