
var table = [
    {x:1, y:1}, {x:2, y:1}, {x:3, y:1}, {x:4, y:1}, {x:5, y:1},
    {x:6, y:1}, {x:7, y:1}, {x:8, y:1}, {x:14, y:1}, {x:15, y:1},
    {x:1, y:2}, {x:2, y:2}, {x:3, y:2}, {x:4, y:2}, {x:5, y:2},
    {x:6, y:2}, {x:7, y:2}, {x:8, y:2}, {x:13, y:2}, {x:14, y:2},
    {x:15, y:2}, {x:6, y:3}, {x:7, y:3}, {x:8, y:3}, {x:14, y:3}, {x:15, y:3},
    {x:7, y:4}, {x:8, y:4}, {x:14, y:4}, {x:15, y:4}, {x:7, y:5},
    {x:8, y:5}, {x:14, y:5}, {x:15, y:5}, {x:6, y:6}, {x:7, y:6},
    {x:8, y:6}, {x:14, y:6}, {x:15, y:6}, {x:1, y:7}, {x:2, y:7},
    {x:3, y:7}, {x:4, y:7}, {x:5, y:7}, {x:6, y:7}, {x:7, y:7},
    {x:8, y:7}, {x:14, y:7}, {x:15, y:7}, {x:1, y:8}, {x:2, y:8},
    {x:3, y:8}, {x:4, y:8}, {x:5, y:8}, {x:6, y:8}, {x:7, y:8},
    {x:8, y:8}, {x:14, y:8}, {x:15, y:8}, {x:6, y:9}, {x:7, y:9},
    {x:8, y:9}, {x:14, y:9}, {x:15, y:9}, {x:7, y:10}, {x:8, y:10},
    {x:14, y:10}, {x:15, y:10}, {x:7, y:11}, {x:8, y:11}, {x:14, y:11},
    {x:15, y:11}, {x:6, y:12}, {x:7, y:12}, {x:8, y:12}, {x:14, y:12},
    {x:15, y:12}, {x:1, y:13}, {x:2, y:13}, {x:3, y:13}, {x:4, y:13},
    {x:5, y:13}, {x:6, y:13}, {x:7, y:13}, {x:8, y:13}, {x:14, y:13},
    {x:15, y:13}, {x:1, y:14}, {x:2, y:14}, {x:3, y:14}, {x:4, y:14},
    {x:5, y:14}, {x:6, y:14}, {x:7, y:14}, {x:8, y:14}, {x:13, y:14},
    {x:14, y:14}, {x:15, y:14}, {x:16, y:14}
];

var camera, scene, renderer;
var controls;

var objects = [];
var targets = { table: [], sphere: [], helix: [], grid: [] };

init();
animate();

function init() {
	camera = new THREE.PerspectiveCamera( 40, window.innerWidth / window.innerHeight, 1, 10000 );
	camera.position.z = 3000;

	scene = new THREE.Scene();

	// table
	for ( var i = 0, len = table.length; i < len; i++ ) {
		var element = document.createElement( 'div' );
		element.className = 'element';
		element.style.backgroundColor = 'rgba(0,127,127,' + ( Math.random() * 0.5 + 0.25 ) + ')';

		var img = document.createElement('img');
		img.style.width = '100%';
		img.style.height = '100%';
		img.src = 'avatar/a' + (Math.floor(Math.random()*25)+1) + '.jpg';
		
		element.appendChild(img);

		var object = new THREE.CSS3DObject( element );
		object.position.x = Math.random() * 4000 - 2000;
		object.position.y = Math.random() * 4000 - 2000;
		object.position.z = Math.random() * 4000 - 2000;
		scene.add( object );

		objects.push( object );

		var object = new THREE.Object3D();
		object.position.x = ( table[i].x * 140 ) - 1330;
		object.position.y = - ( table[i].y * 180 ) + 990;

		targets.table.push( object );
	}

	// sphere
	var vector = new THREE.Vector3();
	for ( var i = 0, l = objects.length; i < l; i ++ ) {
		var phi = Math.acos( -1 + ( 2 * i ) / l );
		var theta = Math.sqrt( l * Math.PI ) * phi;

		var object = new THREE.Object3D();

		object.position.x = 800 * Math.cos( theta ) * Math.sin( phi );
		object.position.y = 800 * Math.sin( theta ) * Math.sin( phi );
		object.position.z = 800 * Math.cos( phi );

		vector.copy( object.position ).multiplyScalar( 2 );

		object.lookAt( vector );

		targets.sphere.push( object );
	}

	// helix
	var vector = new THREE.Vector3();
	for ( var i = 0, l = objects.length; i < l; i ++ ) {
		var phi = i * 0.175 + Math.PI;

		var object = new THREE.Object3D();

		object.position.x = 900 * Math.sin( phi );
		object.position.y = - ( i * 8 ) + 450;
		object.position.z = 900 * Math.cos( phi );

		vector.x = object.position.x * 2;
		vector.y = object.position.y;
		vector.z = object.position.z * 2;

		object.lookAt( vector );

		targets.helix.push( object );
	}

	// grid
	for ( var i = 0; i < objects.length; i ++ ) {
		var object = new THREE.Object3D();

		object.position.x = ( ( i % 5 ) * 400 ) - 800;
		object.position.y = ( - ( Math.floor( i / 5 ) % 5 ) * 400 ) + 800;
		object.position.z = ( Math.floor( i / 25 ) ) * 1000 - 2000;

		targets.grid.push( object );
	}

	renderer = new THREE.CSS3DRenderer();
	renderer.setSize( window.innerWidth, window.innerHeight );
	renderer.domElement.style.position = 'absolute';
	document.getElementById( 'container' ).appendChild( renderer.domElement );

	controls = new THREE.TrackballControls( camera, renderer.domElement );
	controls.rotateSpeed = 0.5;
	controls.minDistance = 500;
	controls.maxDistance = 6000;
	controls.addEventListener( 'change', render );
	
	window.addEventListener( 'resize', onWindowResize, false );
}

function transform( targets, duration ) {
	TWEEN.removeAll();

	for ( var i = 0; i < objects.length; i ++ ) {
		var object = objects[ i ];
		var target = targets[ i ];

		new TWEEN.Tween( object.position )
			.to( { x: target.position.x, y: target.position.y, z: target.position.z }, Math.random() * duration + duration )
			.easing( TWEEN.Easing.Exponential.InOut )
			.start();

		new TWEEN.Tween( object.rotation )
			.to( { x: target.rotation.x, y: target.rotation.y, z: target.rotation.z }, Math.random() * duration + duration )
			.easing( TWEEN.Easing.Exponential.InOut )
			.start();
	}

	new TWEEN.Tween( this )
		.to( {}, duration * 2 )
		.onUpdate( render )
		.start();
}

function onWindowResize() {
	camera.aspect = window.innerWidth / window.innerHeight;
	camera.updateProjectionMatrix();

	renderer.setSize( window.innerWidth, window.innerHeight );

	render();
}

var y = 400,
	MaxRange = 2500,
	t = 0;
function animate() {
	requestAnimationFrame( animate );

	TWEEN.update();

	controls.update();
	
	t++;
    camera.position.set( MaxRange*Math.sin(t/180), y, MaxRange*Math.cos(t/180));
    camera.lookAt( {x:0, y:0, z:0 } );
    renderer.render(scene, camera);
}

function render() {
	renderer.render( scene, camera );
}

$(function(){
	var effects = [
	    { type: 'table', range: 4400 },
		{ type: 'sphere', range: 2400 },
		{ type: 'helix', range: 2200 },
		{ type: 'grid', range: 2400 }
	];
	var innerTimer = null;
	setInterval(function(){
		clearInterval(innerTimer);
		var randomEffect = effects[Math.floor(Math.random()*effects.length)];
		transform( targets[randomEffect.type], 2400 );
		
		innerTimer = setInterval(function(){
			if(MaxRange > randomEffect.range){
				MaxRange -= 5;
			} else if (MaxRange < randomEffect.range){
				MaxRange += 5;
			}
		}, 4);
	}, 5000);
	
	setInterval(function(){
		var idx = Math.floor(Math.random()*25)+1;
		var $I = $('<img src="avatar/a'+idx+'.jpg">');
		$('body').append($I);
		setTimeout(function(){
			$I.addClass('img-animate');
		}, 10);
	}, 3000);
});

