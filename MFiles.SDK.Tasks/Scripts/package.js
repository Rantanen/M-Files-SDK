
module = function ( name, definition ) {
    /// <summary>Define a reusable module.</summary>
    var m = using( name );
    m = new definition( m );
    return m;
};

using = function ( module ) {
    var parts = module.split( "." );
    var parent = this;
    for( var i in parts ) {
        var part = parts[i];
        if( !parent[part] ) parent[part] = {};
        parent = parent[part];
    }

    return parent;
};
