import { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
const EmpEdit = () => {
    const {empid} = useParams();
    const[empdata,empdatachange] = useState({})
    const[id,idchange] = useState("");
    const[dateofbirth,dateofbirthchange] = useState("");
    const[firstname,firstnamechange] = useState("");
    const[lastname,lastnamechange] = useState("");
    const[salary,salarychange] = useState("");
    const navigate = useNavigate();
    const[validation, valchange] = useState(false);
    const LoadRelatedDep=(employeeId) => {
        navigate('/dependents/listing/' + employeeId);
    }
    const handlesubmit= (e) => {
        e.preventDefault();
        const empdata = {id,firstname,lastname,salary, dateofbirth};
        //console.log(empdata);
        
        fetch("https://localhost:7124/api/v1/Employees/" + empid,
        {
            method: "PUT",
            headers: {"content-type" : "application/json"},
            body: JSON.stringify(empdata)
        }
        ).then((res) => {
            alert("Saved Successfully");
            navigate('/')
        }).catch((err) => {
            console.log(err.message)
        }) 
    }
    useEffect(() =>{
        fetch("https://localhost:7124/api/v1/Employees/" + empid).then((res) => {
            return res.json();
        }).then((resp) =>{
            idchange(resp.data.id);
            firstnamechange(resp.data.firstName);
            lastnamechange(resp.data.lastName);
            salarychange(resp.data.salary);
            dateofbirthchange(resp.data.dateOfBirth);
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
                            <h2>Employee Edit</h2>
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
                                        <label>Salary</label>
                                        <input value={salary} onChange={e=>salarychange(e.target.value)} className="form-control"></input>
                                    </div>
                                </div>
                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>Date Of Birth</label>
                                        <input value={dateofbirth}  disabled="disabled" onChange={e=>salarychange(e.target.value)} className="form-control"></input>
                                    </div>
                                </div>
                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <button className="btn btn-success" type="submit">Save</button>
                                        <Link to="/" className="btn btn-danger">Back</Link>
                                        <a onClick={() => {LoadRelatedDep(id)}} className="btn btn-primary">Add/Edit Dependent(s)</a>
                                        
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
export default EmpEdit;