import React, { Component } from 'react';
import * as THREE from 'three';
import metal_texture from '../img/metal_floor_texture.jpg'
import productionline_glb from './objects/ProductionLine.glb'
import machine_glb from './objects/MachineDesign.glb'
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader';

var scene, machineOffset = 0, dirLight, dirLightHeper, hemiLight, hemiLightHelper;
var mixers = [];
var clock = new THREE.Clock();


class SceneManager extends Component {

    componentDidMount() {
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
            1000
        )
        this.camera.position.z = 10;
        this.camera.position.y = 10;

        //ADD FLOOR
        const texture = new THREE.TextureLoader().load(metal_texture);
        texture.wrapS = texture.wrapT = THREE.RepeatWrapping;
        texture.repeat.set(25, 25);
        texture.anisotropy = 16;
        const geometryPlane = new THREE.PlaneBufferGeometry(300, 400, 1);
        const materialFloorTexture = new THREE.MeshBasicMaterial({ map: texture, side: THREE.DoubleSide });
        this.meshPlane = new THREE.Mesh(geometryPlane, materialFloorTexture);
        this.meshPlane.rotateX(Math.PI / 2);
        this.meshPlane.receiveShadow = true;
        //scene.add(this.meshPlane);


        //ADD WALLS
        const wall = new THREE.PlaneBufferGeometry(300, 100, 1);
        this.meshWall = new THREE.Mesh(wall, materialFloorTexture);
        this.meshWall.translateZ(-200);
        this.meshWall.translateY(50);
        //scene.add(this.meshWall);
        const wall2 = new THREE.PlaneBufferGeometry(400, 100, 1);
        this.meshWall2 = new THREE.Mesh(wall2, materialFloorTexture);
        this.meshWall2.rotateY(Math.PI / 2);
        this.meshWall2.translateZ(-150);
        this.meshWall2.translateY(50);
        //scene.add(this.meshWall2);

        // GROUND

        var groundGeo = new THREE.PlaneBufferGeometry(10000, 10000);
        var groundMat = new THREE.MeshLambertMaterial({ color: 0xffffff });
        groundMat.color.setHSL(0.095, 1, 0.75);
        var ground = new THREE.Mesh(groundGeo, groundMat);
        //ground.position.y = - 10;
        ground.rotation.x = - Math.PI / 2;
        ground.receiveShadow = true;
        scene.add(ground);

        //var light = new THREE.AmbientLight(0x404040, 1); // soft white light
        //scene.add(light);
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
        dirLightHeper = new THREE.DirectionalLightHelper(dirLight, 10);
        scene.add(dirLightHeper);

        //ADD PRODUCTION LINE
        this.createProductionLine();
        //ADD MACHINE
        this.createMachine();
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
            console.log(gltf.scene.children)

            var meshMachine = gltf.scene.children[2];
            var meshtool1 = gltf.scene.children[0];
            var meshtool2 = gltf.scene.children[1];
            //meshtool2.parent = meshMachine;
            //meshtool2.position.set(0,10,2.5);
            //meshMachine.translateZ(2.5);
            //meshMachine.translateY(10 + machineOffset);
            meshMachine.castShadow = true;
            meshMachine.receiveShadow = true;
            //meshtool1.translateY(10 + machineOffset);
            //meshtool1.translateZ(2.5);
            //meshtool2.translateY(10 + machineOffset);
            //meshtool2.translateZ(2.5);
            //machineOffset -= 3;
            scene.add(meshMachine);
            var pivotPoint = new THREE.Object3D();
            meshMachine.add(pivotPoint);
            pivotPoint.add(meshtool2);
            //meshMachine.add(meshtool2);
            //scene.add(meshtool1);
            scene.add(meshtool2);
            //var mixersMachine = new THREE.AnimationMixer(meshtool2);
            console.log(gltf)
            //mixersMachine.clipAction(gltf.animations[1]).setDuration(1).play();
            //mixers.push(mixersMachine);
            //var mixersMachine = new THREE.AnimationMixer(meshtool1);
            //mixersMachine.clipAction(gltf.animations[1]).setDuration(1).play();
            //mixers.push(mixersMachine);
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
        var delta = clock.getDelta();
        for (var i = 0; i < mixers.length; i++) {
            mixers[i].update(delta);
        }
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