import React, { useState } from 'react';
import { Navbar, Nav } from 'react-bootstrap';
import MachineTypes from './MachineType';
import Machines from './Machine';
import Operations from './Operation';
import Products from './Product';
import SceneManager from '../three/SceneManager';

const NavigationBar = () => {

	const [machinesView, setMachinesView] = useState(false)
	const [machineTypesView, setMachineTypesView] = useState(false)
	const [productsView, setProductsView] = useState(false)
	const [operationsView, setOperationsView] = useState(false)
	const [visualizationView, setVisualizationView] = useState(false)

	const hideAllViews = () => {
		setMachinesView(false)
		setMachineTypesView(false)
		setProductsView(false)
		setOperationsView(false)
		setVisualizationView(false)
		return true
	}

	return (
		<>
			<div>
				<Navbar bg="dark" variant="dark">
					<Navbar.Brand>My Own Cutlery</Navbar.Brand>
					<Nav className="mr-auto">
						<Nav.Link onClick={() => hideAllViews() && setMachinesView(true)}>Machines</Nav.Link>
						<Nav.Link onClick={() => hideAllViews() && setMachineTypesView(true)} >Machine Types</Nav.Link>
						<Nav.Link onClick={() => hideAllViews() && setProductsView(true)}>Products</Nav.Link>
						<Nav.Link onClick={() => hideAllViews() && setOperationsView(true)}>Operations</Nav.Link>
						<Nav.Link onClick={() => hideAllViews() && setVisualizationView(true)}>Visualization</Nav.Link>
					</Nav>
				</Navbar>
				<div class="center">
					{machinesView && <Machines />}
					{machineTypesView && <MachineTypes />}
					{productsView && <Products />}
					{operationsView && <Operations />}
					{visualizationView && <SceneManager />}
				</div>
			</div>
		</>
	)
}

export default NavigationBar