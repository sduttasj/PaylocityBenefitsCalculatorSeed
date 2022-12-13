import logo from './logo.svg';
import './App.css';
import {BrowserRouter, Route, Routes} from 'react-router-dom'
import EmpListing from './EmpListing'
import EmpDetail from './EmpPaycheckView'
import EmpCreate from './EmpCreate'
import EmpEdit from './EmpEdit'
import DepCreate from './DepCreate'
import DepEdit from './DepEdit'
import DepListing from './DepListing'
import EmpPaycheckView from './EmpPaycheckView'
import EmpPaycheckCalculate from './EmpPaycheckCalculate'
import DepListingForAnEmployee from './DepListingForAnEmployee';
function App() {
  return (
    <div className="App">
      <h1>Paylocity Benefits Calculator</h1>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<EmpListing/>}></Route>
          <Route path='/employee/create' element={<EmpCreate/>}></Route>
          <Route path='/employee/paycheck/:empid' element={<EmpPaycheckView/>}></Route>
          <Route path='/employee/paycheck/calculations/:empid' element={<EmpPaycheckCalculate/>}></Route>
          <Route path='/employee/edit/:empid' element={<EmpEdit/>}></Route>
          <Route path='/dependents/create/:employeeId' element={<DepCreate/>}></Route>
          <Route path='/dependents/edit/:depid' element={<DepEdit/>}></Route>
          <Route path='/dependents/listing' element={<DepListing/>}></Route>
          <Route path='/dependents/listing/:empid' element={<DepListingForAnEmployee/>}></Route>
          <Route path='/employee/listing' element={<EmpListing/>}></Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
  
}

export default App;
