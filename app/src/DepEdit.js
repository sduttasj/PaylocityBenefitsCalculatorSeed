import { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { Relationship,formatDate,ReverseRelationship } from "./Constants";
const DepEdit = () => {
    const {depid} = useParams();
    const[depdata,depdatachange] = useState({})
    const[id,idchange] = useState("");
    const[firstname,firstnamechange] = useState("");
    const[lastname,lastnamechange] = useState("");
    const[dateofbirth,dateofbirthchange] = useState("");
    const[relationship,relationshipchange] = useState("");
    const navigate = useNavigate();
    const[validation, valchange] = useState(false);
    
    const handlesubmit= (e) => {
        e.preventDefault();
        const depdata = {firstname,lastname,dateofbirth,relationship};
        var relationshipVal = depdata.relationship;
        depdata.relationship = ReverseRelationship[relationshipVal];
        //console.log("depid is :" + depid);
        
        //console.log(JSON.stringify(depdata));
        //return;
        fetch("https://localhost:7124/api/v1/Dependents/" + depid,
        {
            method: "PUT",
            headers: {"content-type" : "application/json"},
            body: JSON.stringify(depdata)
        }
        ).then((res) => {
            alert("Saved Successfully");
            navigate('/dependents/listing')
        }).catch((err) => {
            console.log(err.message)
        })
    }
    useEffect(() =>{
        fetch("https://localhost:7124/api/v1/Dependents/" + depid).then((res) => {
            return res.json();
        }).then((resp) =>{
            //console.log(resp.data.relationship);
            idchange(resp.data.id);
            firstnamechange(resp.data.firstName);
            lastnamechange(resp.data.lastName);
            var badDate = new Date(resp.data.dateOfBirth);
            dateofbirthchange(formatDate(badDate));
            relationshipchange(Relationship[resp.data.relationship]);
        }).catch((err) => {
            console.log(err.message);
        })
    },[]);
    return ( 
        <div>
            <div className="row">
              <div className="offset-lg-3 col-lg-6">
                <form className="container" onSubmit={handlesubmit}>
                    <div className="card" style={{"textAlign":"left"}}>
                        <div className="card-title">
                            <h2>Dependent Edit</h2>
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
                                        <input required value={firstname} onMouseDown={e=>valchange(true)} onChange={e=>firstnamechange(e.target.value)} className="form-control"></input>
                                        {firstname.length == 0 && validation && <span className="text-danger">Enter the name</span>}
                                    </div>
                                </div>
                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>Last Name</label>
                                        <input value={lastname} onChange={e=>lastnamechange(e.target.value)} className="form-control"></input>
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
                                        <label>Date Of Birth</label>
                                        <input type="date" value={dateofbirth} onChange={e=>dateofbirthchange(e.target.value)} className="form-control"></input>
                                    </div>
                                </div>
                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <button className="btn btn-success" type="submit">Save</button>
                                        <Link to="/dependents/listing" className="btn btn-danger">Back</Link>
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
export default DepEdit;