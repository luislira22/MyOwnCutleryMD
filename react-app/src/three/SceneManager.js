import React, { Component } from 'react';
import {ButtonToolbar, Button} from 'react-bootstrap';
import * as THREE from 'three';
import metal_texture from '../img/metal_floor_texture_2.jpg'
import productionline_glb from './objects/ProductionLine.gltf'
import machine_glb from './objects/MachineDesign.gltf'
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader';
import { Scene } from 'three';

var raycaster = new THREE.Raycaster();
var mouse = new THREE.Vector2(), INTERSECTED;
var scene, machineOffset = 0, dirLight, dirLightHeper, hemiLight, hemiLightHelper;
var mixers = [];
var clock = new THREE.Clock();
var machines = [];

class SceneManager extends Component {

    componentDidMount() {
        //machineOffset = 0;
        const width = this.mount.clientWidth
        const height = this.mount.clientHeight
        //ADD SCENE
        scene = new THREE.Scene()
        scene.background = new THREE.Color().setHSL(0.6, 0, 1);
        //scene.background = new THREE.Color(0xe8fffe);
        //ADD CAMERA
        this.camera = new THREE.PerspectiveCamera(
            75,
            width / height,
            0.1,
            200
        )
        this.camera.position.z = 10;
        this.camera.position.y = 10;

        //ADD FLOOR
        const texture = new THREE.TextureLoader().load(metal_texture);
        texture.wrapS = texture.wrapT = THREE.RepeatWrapping;
        texture.repeat.set(200, 200);
        texture.anisotropy = 16;
        const geometryPlane = new THREE.PlaneBufferGeometry(300, 300, 1);
        const materialFloorTexture = new THREE.MeshLambertMaterial({ map: texture, side: THREE.DoubleSide });
        this.meshPlane = new THREE.Mesh(geometryPlane, materialFloorTexture);
        this.meshPlane.name = "metalfloor";
        this.meshPlane.rotateX(Math.PI / 2);
        this.meshPlane.receiveShadow = true;
        scene.add(this.meshPlane);


        //ADD WALLS
        const wall = new THREE.PlaneBufferGeometry(40, 10, 1);
        this.meshWall = new THREE.Mesh(wall, materialFloorTexture);
        this.meshWall.translateZ(-20);
        this.meshWall.translateY(5);
        scene.add(this.meshWall);
        this.meshWall2 = new THREE.Mesh(wall, materialFloorTexture);
        this.meshWall2.rotateY(Math.PI / 2);
        this.meshWall2.translateZ(20);
        this.meshWall2.translateY(5);
        scene.add(this.meshWall2);
        this.meshWall3 = new THREE.Mesh(wall, materialFloorTexture);
        this.meshWall3.rotateY(Math.PI / 2);
        this.meshWall3.translateZ(-20);
        this.meshWall3.translateY(5);
        scene.add(this.meshWall3);
        this.meshWall4 = new THREE.Mesh(wall, materialFloorTexture);
        this.meshWall4.translateZ(20);
        this.meshWall4.translateY(5);
        scene.add(this.meshWall4);

        // GROUND
        var groundGeo = new THREE.PlaneBufferGeometry(10000, 10000);
        var groundMat = new THREE.MeshLambertMaterial({ color: 0xffffff });
        groundMat.color.setHSL(0.67, 0.06, 0.5);
        var ground = new THREE.Mesh(groundGeo, groundMat);
        ground.rotation.x = - Math.PI / 2;
        this.meshPlane.name = "floor";
        ground.translateZ(-1);
        ground.receiveShadow = true;
        scene.add(ground);

        //var light = new THREE.AmbientLight(0x404040, 1); // soft white light
        //scene.add(light);
        //ADD LIGHTS
        hemiLight = new THREE.HemisphereLight(0xffffff, 0xffffff, 1);
        hemiLight.color.setHSL(0.6, 1, 0.6);
        hemiLight.groundColor.setHSL(0.095, 1, 0.75);
        hemiLight.position.set(0, 10, 0);
        scene.add(hemiLight);
        hemiLightHelper = new THREE.HemisphereLightHelper(hemiLight, 1);
        scene.add(hemiLightHelper);
        dirLight = new THREE.DirectionalLight(0xffffff, 1);
        dirLight.color.setHSL(0.1, 1, 0.95);
        dirLight.position.set(-1, 1, 1);
        dirLight.position.multiplyScalar(30);
        scene.add(dirLight);
        //this.camera.add(dirLight);
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
        dirLightHeper = new THREE.DirectionalLightHelper(dirLight, 1);
        scene.add(dirLightHeper);
        //ADD PRODUCTION LINE
        this.createProductionLine();
        //ADD MACHINE
        //this.createMachine();
        //this.createMachine();
        //ADD RENDERER
        this.renderer = new THREE.WebGLRenderer({ antialias: true })

        this.renderer.gammaInput = true;
        this.renderer.gammaOutput = true;
        this.renderer.shadowMap.enabled = true;
        //this.renderer.setClearColor(0x0077ff, 1);
        this.renderer.setSize(width, height)
        this.mount.appendChild(this.renderer.domElement)
        //ADD ORBIT CONTROLS
        this.controls = new OrbitControls(this.camera, this.renderer.domElement);
        this.controls.addEventListener('change', () => this.renderer.render(scene, this.camera));
        window.addEventListener('mousemove', this.onMouseMove, false);
        this.start();
    }

    createProductionLine() {
        var loader = new GLTFLoader();
        loader.load(productionline_glb, function (gltf) {
            var meshBase = gltf.scene.children[2];
            var meshPassadeira = gltf.scene.children[1];
            meshBase.castShadow = true;
            meshBase.receiveShadow = true;
            scene.add(meshBase);
            meshPassadeira.castShadow = true;
            meshPassadeira.receiveShadow = true;
            scene.add(meshPassadeira);
            var mixerPassadeira = new THREE.AnimationMixer(meshPassadeira);
            mixerPassadeira.clipAction(gltf.animations[0]).setDuration(1).play();
            mixers.push(mixerPassadeira);
        });
    }

    createMachine() {
        var loader = new GLTFLoader();
        loader.load(machine_glb, function (gltf) {
            /*scene.add(gltf.scene);
            machines.push(gltf.scene);*/
            
            var meshMachine = gltf.scene.children[2];
            var meshtool1 = gltf.scene.children[0];
            var meshtool2 = gltf.scene.children[1];
            meshMachine.add(meshtool1);
            meshMachine.add(meshtool2);
            meshMachine.translateZ(2.5);
            meshMachine.translateY(10 + machineOffset);
            meshtool1.translateZ(2.5);
            meshtool1.translateY(10 + machineOffset);
            meshtool2.translateZ(2.5);
            meshtool2.translateY(10 + machineOffset);
            meshMachine.castShadow = true;
            meshMachine.receiveShadow = true;
            machineOffset -= 3;
            scene.add(meshtool1);
            scene.add(meshtool2);
            scene.add(meshMachine);
            console.log(meshMachine);
            machines.push(meshMachine,meshtool1,meshtool2);
            /*
            var mixersMachine = new THREE.AnimationMixer(meshtool2);
            mixersMachine.clipAction(gltf.animations[1]).setDuration(1).play();
            mixers.push(mixersMachine);
            var mixersMachine = new THREE.AnimationMixer(meshtool1);
            mixersMachine.clipAction(gltf.animations[1]).setDuration(1).play();
            mixers.push(mixersMachine);*/
        });
    }

    deleteMachines() {
        console.log(machines);
        machines.forEach(element => {
            for (var i = element.children.length - 1; i >= 0; i--) {
                //element.remove(element.children[i]);
                scene.remove(i);
            }
            scene.remove(element);
        })
        machineOffset = 0;
    }

    onMouseMove(event) {

        event.preventDefault();

        mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
        mouse.y = - (event.clientY / window.innerHeight) * 2 + 1;

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
        var delta = clock.getDelta();
        for (var i = 0; i < mixers.length; i++) {
            mixers[i].update(delta);
        }

        this.renderer.render(scene, this.camera)
    }

    render() {
        return (
            <>
            <div
                style={{ width: '1500px', height: '600px' }}
                ref={(mount) => { this.mount = mount }}

            />
            <ButtonToolbar className="center">
                <Button onClick={this.createMachine} variant="outline-primary">Create Machine</Button>
                <Button onClick={this.deleteMachines} variant="outline-primary">Delete Machines</Button>
            </ButtonToolbar>
            </>
        )
    }
}

export default SceneManager;