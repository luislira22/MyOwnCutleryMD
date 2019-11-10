import React, { Component } from 'react';
import * as THREE from 'three';
import metal_texture from '../img/metal_floor_texture.jpg'
import machine_obj from './obj/MachineDesign.obj'
import machine_material from './obj/MachineDesign.mtl'
import productionline from './obj/ProductionLine.obj'
import productionlinematerial from './obj/ProductionLine.mtl'
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import { OBJLoader } from 'three/examples/jsm/loaders/OBJLoader.js';
import { MTLLoader } from 'three/examples/jsm/loaders/MTLLoader.js';

var scene, machineOffset = 0, dirLight, dirLightHeper, hemiLight, hemiLightHelper;

class SceneManager extends Component {

    componentDidMount() {
        const width = this.mount.clientWidth
        const height = this.mount.clientHeight
        //ADD SCENE
        scene = new THREE.Scene()
        //scene.background = new THREE.Color().setHSL(0.6, 0, 1);
        scene.background = new THREE.Color(0xe8fffe);
        //ADD CAMERA
        this.camera = new THREE.PerspectiveCamera(
            75,
            width / height,
            0.1,
            1000
        )
        this.camera.position.z = 200
        //ADD FLOOR
        const texture = new THREE.TextureLoader().load(metal_texture);
        texture.wrapS = texture.wrapT = THREE.RepeatWrapping;
        texture.repeat.set(25, 25);
        texture.anisotropy = 16;
        const geometryPlane = new THREE.PlaneBufferGeometry(300, 400, 1);
        const materialFloorTexture = new THREE.MeshBasicMaterial({ map: texture, side: THREE.DoubleSide });
        this.meshPlane = new THREE.Mesh(geometryPlane, materialFloorTexture);
        this.meshPlane.rotateX(Math.PI / 2);
        scene.add(this.meshPlane);

        //ADD WALLS
        const wall = new THREE.PlaneBufferGeometry(300, 100, 1);
        this.meshWall = new THREE.Mesh(wall, materialFloorTexture);
        this.meshWall.translateZ(-200);
        this.meshWall.translateY(50);
        scene.add(this.meshWall);
        const wall2 = new THREE.PlaneBufferGeometry(400, 100, 1);
        this.meshWall2= new THREE.Mesh(wall2, materialFloorTexture);
        this.meshWall2.rotateY(Math.PI /2);
        this.meshWall2.translateZ(-150);
        this.meshWall2.translateY(50);
        scene.add(this.meshWall2);


        // GROUND
        var groundGeo = new THREE.PlaneBufferGeometry(10000, 10000);
        var groundMat = new THREE.MeshLambertMaterial({ color: 0xffffff });
        groundMat.color.setHSL(0.095, 1, 0.75);
        var ground = new THREE.Mesh(groundGeo, groundMat);
        ground.position.y = - 33;
        ground.rotation.x = - Math.PI / 2;
        ground.receiveShadow = true;
        scene.add(ground);

        var light = new THREE.AmbientLight(0x404040, 1); // soft white light
        scene.add(light);

        //ADD LIGHTS
        hemiLight = new THREE.HemisphereLight(0xffffff, 0xffffff, 0.6);
        hemiLight.color.setHSL(0.6, 1, 0.6);
        hemiLight.groundColor.setHSL(0.095, 1, 0.75);
        hemiLight.position.set(0, 100, 0);
        scene.add(hemiLight);
        hemiLightHelper = new THREE.HemisphereLightHelper(hemiLight, 10);
        scene.add(hemiLightHelper);
        dirLight = new THREE.DirectionalLight(0xffffff, 1);
        dirLight.color.setHSL(0.1, 1, 0.95);
        dirLight.position.set(- 1.5, 2.75, 1);
        dirLight.position.multiplyScalar(30);
        scene.add(dirLight);
        dirLight.castShadow = true;
        dirLight.shadow.mapSize.width = 2048;
        dirLight.shadow.mapSize.height = 2048;
        var d = 50;
        dirLight.shadow.camera.left = - d;
        dirLight.shadow.camera.right = d;
        dirLight.shadow.camera.top = d;
        dirLight.shadow.camera.bottom = - d;
        dirLight.shadow.camera.far = 3500;
        dirLight.shadow.bias = - 0.0001;
        //dirLightHeper = new THREE.DirectionalLightHelper(dirLight, 10);
        //scene.add(dirLightHeper);


        //ADD PRODUCTION LINE
        this.createProductionLine();
        //ADD MACHINE
        this.createMachine();
        this.createMachine();
        //ADD RENDERER
        this.renderer = new THREE.WebGLRenderer({ antialias: true })
        //this.renderer.setClearColor(0x0077ff, 1);
        this.renderer.setSize(width, height)
        this.mount.appendChild(this.renderer.domElement)
        //ADD ORBIT CONTROLS
        this.controls = new OrbitControls(this.camera, this.renderer.domElement);
        this.controls.addEventListener('change', () => this.renderer.render(scene, this.camera));
        this.start();
    }

    createProductionLine() {

        let loader = new OBJLoader();
        let mtlloader = new MTLLoader();


        mtlloader.load(productionlinematerial, function (materials) {

            materials.preload();

            loader.setMaterials(materials);
            loader.load(productionline, function (object) {

                object.scale.set(10, 10, 10);
                //object.translateZ(30);
                //object.translateX(distance);
                //object.rotateX(Math.PI /2)
                scene.add(object);
                console.log(object);

            }, null, null);

        });
    }

    createMachine() {
        let loader = new OBJLoader();
        let mtlloader = new MTLLoader();
        mtlloader.load(machine_material, function (materials) {

            materials.preload();

            loader.setMaterials(materials);
            loader.load(machine_obj, function (object) {
                object.castShadow = true;
                object.scale.set(10, 10, 10);
                object.translateZ(30);
                object.translateX(machineOffset);
                machineOffset += 30;
                //object.rotateX(Math.PI /2)
                scene.add(object);
                console.log(object);

            }, null, null);

        });
    }

    componentWillUnmount() {
        this.stop()
        this.mount.removeChild(this.renderer.domElement)
    }
    start = () => {
        if (!this.frameId) {
            this.frameId = requestAnimationFrame(this.animate)
        }
    }
    stop = () => {
        cancelAnimationFrame(this.frameId)
    }
    animate = () => {
        this.renderScene()
        this.frameId = window.requestAnimationFrame(this.animate)
    }
    renderScene = () => {
        this.renderer.render(scene, this.camera)
    }
    render() {
        return (
            <div
                style={{ width: '1500px', height: '600px' }}
                ref={(mount) => { this.mount = mount }}
            />
        )
    }
}

export default SceneManager;

/*
export default scene => {

    var camera, scene, renderer;
    var meshBox, meshPlane,loader,mtlloader;

    camera = new THREE.PerspectiveCamera(70, window.innerWidth / window.innerHeight, 1, 1000);
    camera.position.z = 200;
    scene = new THREE.Scene();
    var texture = new THREE.TextureLoader().load(metal_texture);
    texture.wrapS = texture.wrapT = THREE.RepeatWrapping;
    texture.repeat.set(25, 25);
    texture.anisotropy = 16;
    var geometryBox = new THREE.BoxBufferGeometry(30, 20, 200);
    var materialFloorTexture = new THREE.MeshBasicMaterial({ map: texture, side: THREE.DoubleSide });
    var geometryPlane = new THREE.PlaneBufferGeometry(300, 400, 1);
    var materialBox = new THREE.MeshBasicMaterial({ color: 0x133337, side: THREE.DoubleSide });
    var helper = new THREE.GridHelper(160, 10);
    meshBox = new THREE.Mesh(geometryBox, materialBox);
    meshPlane = new THREE.Mesh(geometryPlane, materialFloorTexture);
    meshPlane.rotation.x = Math.PI / 2;
    meshBox.translateY(10);
    scene.add(helper);
    scene.add(meshPlane);
    scene.add(meshBox);
    createMachine(35);
    createMachine(-35);

    var ambient = new THREE.AmbientLight(0x444444);
    scene.add(ambient);

    var directionalLight = new THREE.DirectionalLight(0xffeedd);
    directionalLight.position.set(0, 0, 1).normalize();
    scene.add(directionalLight);

    renderer = new THREE.WebGLRenderer({ antialias: true });
    let controls = new OrbitControls(camera, renderer.domElement);
    renderer.setClearColor(0xffffff, 1);
    renderer.setPixelRatio(window.devicePixelRatio);
    renderer.setSize(window.innerWidth, window.innerHeight);

    document.body.appendChild(renderer.domElement);
    window.addEventListener('resize', onWindowResize, false);
    controls.addEventListener('change', () => renderer.render(scene, camera));

    function onWindowResize() {
        camera.aspect = window.innerWidth / window.innerHeight;
        camera.updateProjectionMatrix();
        renderer.setSize(window.innerWidth, window.innerHeight);
    }

    function render() {
        requestAnimationFrame(render);
        renderer.render(scene, camera);
    }

    function createMachine(distance) {
       loader = new OBJLoader2();
       loader.load(machine_obj, function (object) {

            object.translateX(distance);
            object.scale.set(0.05, 0.05, 0.05);
            scene.add(object);
            console.log(object);

        });
       loader = new OBJLoader2();
       mtlloader = newmtlloader();
       mtlloader.load(machine_material, function (materials) {

            materials.preload();

           loader.setMaterials(materials);
           loader.load(machine_obj, function (object) {

                object.translateX(distance);
                object.scale.set(0.05, 0.05, 0.05);
                scene.add(object);
                console.log(object);

            }, null, null);

        });
    }
    //init();
    render();
}
*/