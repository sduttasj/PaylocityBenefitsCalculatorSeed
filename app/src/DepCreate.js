import { useEffect, useState } from "react";
import { useParams,Link, useNavigate } from "react-router-dom";
import { ReverseRelationship } from "./Constants";
/*
{
  "firstName": "string",
  "lastName": "string",
  "dateOfBirth": "2022-12-13T02:14:42.659Z",
  "relationship": 2,
  "employeeId": 17
}*/
const DepCreate = () => {
    const[id,idchange] = useState("");
    const {employeeId} = useParams();
    const[firstName,firstnamechange] = useState("");
    const[lastName,lastnamechange] = useState("");
    const[dateOfBirth,dateofbirthchange] = useState("");
    const[relationship,relationshipchange] = useState("");
    const navigate = useNavigate();
    const[validation, valchange] = useState(false);
    const handlesubmit= (e) => {
        e.preventDefault();
        const depdata = {id,firstName,lastName,dateOfBirth, relationship,employeeId};
        var relationshipVal = depdata.relationship;
        depdata.relationship = ReverseRelationship[relationshipVal];
        console.log(depdata);
        
        fetch("https://localhost:7124/api/v1/Dependents/",
        {
            method: "POST",
            headers: {"content-type" : "application/json"},
            body: JSON.stringify(depdata)
        }
        ).then((res) => {
            alert("Saved Successfully");
            navigate('/dependents/listing/'  + employeeId)
        }).catch((err) => {
            console.log(err.message)
        })
    }
    return ( 
        <div>
            <div className="row">
              <div className="offset-lg-3 col-lg-6">
                <form className="container" onSubmit={handlesubmit}>
                    <div className="card" style={{"textAlign":"left"}}>
                        <div className="card-title">
                            <h2>Dependent Create</h2>
                        </div>
                        <div className="card-body">
                            <div className="row">
                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>ID</label>
                                        <input value={id} disabled="disabled" className="form-control"></input>
                                    </div>
                                </div>
                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>First Name</label>
                                        <input required value={firstName} onMouseDown={e=>valchange(true)} onChange={e=>firstnamechange(e.target.value)} className="form-control"></input>
                                        {firstName.length == 0 && validation && <span className="text-danger">Enter the First name</span>}
                                    </div>
                                </div>
                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>Last Name</label>
                                        <input value={lastName} onChange={e=>lastnamechange(e.target.value)} className="form-control"></input>
                                    </div>
                                </div>
                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>Date of Birth</label>
                                        <input required type="date" value={dateOfBirth} onChange={e=>dateofbirthchange(e.target.value)} className="form-control"></input>
                                    </div>
                                </div>
                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>Relationship</label>
                                        <select value={relationship} onChange={e=>relationshipchange(e.target.value)} className="form-control">
                                            <option></option>
                                            <option value="Spouse">Spouse</option>
                                            <option value="DomesticPartner">DomesticPartner</option>
                                            <option value="Child">Child</option>
                                        </select>
                                    </div>
                                </div>
                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <button className="btn btn-success" type="submit">Save</button>
                                        <Link to="/" className="btn btn-danger">Back</Link>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </form>
              </div>
            </div>

        </div>
    );
}
export default DepCreate;