import { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { Relationship } from "./Constants";
const DepListingForAnEmployee = () => {
    const {empid} = useParams();
    const[depdata, depdatachange] = useState(null);
    const navigate=useNavigate();
    const LoadEdit=(id) => {
        navigate('/dependents/edit/' + id);
    }
    const LoadDependentWizard=(empid) => {
        navigate('/dependents/create/' +empid);
    }
    const Remove=(id) => {
        if (window.confirm('Do you want to remove?')){
        fetch("https://localhost:7124/api/v1/Dependents/" + id,
        {
            method: "DELETE"
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
        fetch("https://localhost:7124/api/v1/Employees/Dependents/" + empid).then((res) => {
            return res.json();
        }).then((resp) =>{
            depdatachange(resp.data);
        }).catch((err) => {
            console.log(err.message);
        })
    },[])
    return (
        <div className="container">
            <div className="card">
                <div className="card-title">
                    <h2>Dependent Listing</h2>
                </div>
                <div className="card-body">
                    <div className="divbtn">
                        <a onClick={() => {LoadDependentWizard(empid)}} className="btn btn-primary">Add Dependent(s)</a>
                    </div>
                    <table className="table table-bordered">
                        <thead className="bg-dark text-white">
                            <tr>
                                <td>ID</td>
                                <td>First Name</td>
                                <td>Last Name</td>
                                <td>Date Of Birth</td>
                                <td>Relationship</td>
                                <td>Action</td>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                depdata && depdata.map(item=>(
                                    <tr key={item.id}>
                                        <td>{item.id}</td>
                                        <td>{item.firstName}</td>
                                        <td>{item.lastName}</td>
                                        <td>{item.dateOfBirth}</td>
                                        <td>{Relationship[item.relationship]}</td>
                                        <td>
                                            <a onClick={() => {LoadEdit(item.id)}} className="btn btn-success">Edit</a>
                                            <a onClick={() => {Remove(item.id)}} className="btn btn-danger">Remove</a>
                                            
                                        </td>
                                    </tr>
                                ))
                            }
                        </tbody>
                    </table>
                    <div className="divbtn">
                        <Link to="/employee/listing" className="btn btn-danger">Back</Link>
                    </div>
                </div>
            </div>

        </div>
    );
}
export default DepListingForAnEmployee;