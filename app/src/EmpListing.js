import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { currencyFormat } from "./Constants";
const EmpListing = () => {
    const[empdata, empdatachange] = useState(null);
    const navigate=useNavigate();
    const ViewPaycheck=(id) => {
        navigate('/employee/paycheck/' + id);
    }
    const CalculatePaycheck=(id) => {
        navigate('/employee/paycheck/calculations/' + id);
    }
    const LoadEdit=(id) => {
        navigate('/employee/edit/' + id);
    }
    const Remove=(id) => {
        if (window.confirm('Do you want to remove?')){
        fetch("https://localhost:7124/api/v1/Employees/" + id,
        {
            method: "DELETE"
        ,
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept, Z-Key',
            'Access-Control-Allow-Methods': 'GET, HEAD, POST, PUT, DELETE, OPTIONS'}
        }
        ).then((res) => {
            alert("Removed Successfully");
            window.location.reload();
        }).catch((err) => {
            console.log(err.message)
        })
        }
    }

    useEffect(() => {
        fetch("https://localhost:7124/api/v1/Employees").then((res) => {
            return res.json();
        }).then((resp) =>{
            console.log(resp);
            empdatachange(resp.data);
        }).catch((err) => {
            console.log(err.message);
        })
    },[])
    return (
        <div className="container">
            <div className="card">
                <div className="card-title">
                    <h1>Employees</h1>
                </div>
                <div className="card-body">
                    <div className="divbtn">
                        <Link to="employee/create" className="btn btn-success">Add New</Link>
                    </div>
                    <div className="divbtn">
                    <Link to="/dependents/listing" className="btn btn-primary">View All Dependents</Link>
                    </div>
                    <table className="table table-bordered">
                        <thead className="bg-dark text-white">
                            <tr>
                                <td>ID</td>
                                <td>First Name</td>
                                <td>Last Name</td>
                                <td>DOB</td>
                                <td>Salary</td>
                                <td>Dependents</td>
                                <td>Action</td>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                empdata && empdata.map(item=>(
                                    <tr key={item.id}>
                                        <td>{item.id}</td>
                                        <td>{item.firstName}</td>
                                        <td>{item.lastName}</td>
                                        <td>{item.dateOfBirth}</td>
                                        <td>{currencyFormat(item.salary)}</td>
                                        <td>{item.dependents?.length || 0}</td>
                                        <td>
                                            <a onClick={() => {LoadEdit(item.id)}} className="btn btn-success">Edit</a>
                                            <a onClick={() => {Remove(item.id)}} className="btn btn-danger">Remove</a>
                                            <a onClick={() => {ViewPaycheck(item.id)}} className="btn btn-primary">View Paycheck</a>
                                            <a onClick={() => {CalculatePaycheck(item.id)}} className="btn btn-primary">Calculate Paycheck</a>
                                        </td>
                                    </tr>
                                ))
                            }
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
    );
}
export default EmpListing;