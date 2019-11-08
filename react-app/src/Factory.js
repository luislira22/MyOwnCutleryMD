import React, { Component } from 'react'
import * as THREE from "three";

export const sceneSetup = () => {
 // === THREE.JS CODE START ===
 /*
    var scene = new THREE.Scene();
    var camera = new THREE.PerspectiveCamera( 75, window.innerWidth/window.innerHeight, 0.1, 1000 );
    var renderer = new THREE.WebGLRenderer();
    renderer.setSize( window.innerWidth, window.innerHeight );
    document.body.appendChild( renderer.domElement );
    var geometry = new THREE.BoxGeometry( 1, 1, 1 );
    var material = new THREE.MeshBasicMaterial( { color: 0x00ff00 } );
    var cube = new THREE.Mesh( geometry, material );
    scene.add( cube );
    camera.position.z = 5;
    var animate = function () {
      requestAnimationFrame( animate );
      cube.rotation.x += 0.01;
      cube.rotation.y += 0.01;
      renderer.render( scene, camera );
    };
    animate();*/
    // === THREE.JS EXAMPLE CODE END ===
};

export default Factory;
/*
let camera, scene, renderer, cube;
//import * as THREE from 'js/three.js';
//import { OrbitControls } from './js/OrbitControls.js';

function init() {
    scene = new THREE.Scene();

	// Init camera (PerspectiveCamera)
	camera = new THREE.PerspectiveCamera(
		75,
		window.innerWidth / window.innerHeight,
		0.1,
		1000
	);

	// Init renderer
	renderer = new THREE.WebGLRenderer({ antialias: true });

	// Set size (whole window)
	renderer.setSize(window.innerWidth, window.innerHeight);

	// Render to canvas element
	document.body.appendChild(renderer.domElement);

	// Init BoxGeometry object (rectangular cuboid)
	const geometry = new THREE.PlaneGeometry(3, 3, 3);

	// Create material with color
	const material = new THREE.MeshBasicMaterial({ color: 0x0000ff,side: THREE.DoubleSide});
	// Add texture - 
	const texture = new THREE.TextureLoader().load('./textures/metal.jpg');

	// Create material with texture
	const material2 = new THREE.MeshBasicMaterial({ map: texture });

	// Create mesh with geo and material
	cube = new THREE.Mesh(geometry, material2);
	// Add to scene
	scene.add(cube);

	// Position camera
	camera.position.z = 5;

    let controls = new THREE.OrbitControls(camera);
    controls.addEventListener('change', renderer);
    //controls.minDistance = 500;
    //controls.maxDistance = 1500;

    
}

// Draw the scene every time the screen is refreshed
function animate() {
    requestAnimationFrame(animate);
    renderer.render(scene, camera);
}

function onWindowResize() {
    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();
    renderer.setSize(window.innerWidth, window.innerHeight);
}

window.addEventListener('resize', onWindowResize, false);

init();
animate();
*/
