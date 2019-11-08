import * as THREE from 'three';
import metal_texture from '../img/metal_floor_texture.jpg'
import machine_obj from '../obj/machine1.obj'
import machine_material from '../obj/MachineDesign.mtl'
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import { OBJLoader2 } from 'three/examples/jsm/loaders/OBJLoader2.js';
import { MTLLoader } from 'three/examples/jsm/loaders/MTLLoader';



export default scene => {

    var camera, scene, renderer;
    var meshBox, meshPlane, loader, mtlloader;

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
        /*loader = new OBJLoader2();
        mtlloader = new MTLLoader();
        mtlloader.load(machine_material, function (materials) {

            materials.preload();
            
            loader.setMaterials(materials);
            loader.load(machine_obj, function (object) {

                object.translateX(distance);
                object.scale.set(0.05, 0.05, 0.05);
                scene.add(object);
                console.log(object);

            }, null, null);

        });*/
    }
    //init();
    render();
}
