import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
const EmpCreate = () => {
    const[id,idchange] = useState("");
    const[firstname,firstnamechange] = useState("");
    const[lastname,lastnamechange] = useState("");
    const[dateofbirth,dateofbirthchange] = useState("");
    const[salary,salarychange] = useState("");
    const navigate = useNavigate();
    const[validation, valchange] = useState(false);
    const handlesubmit= (e) => {
        e.preventDefault();
        const empdata = {id,firstname,lastname,dateofbirth,salary};
        
        fetch("https://localhost:7124/api/v1/Employees",
        {
            method: "POST",
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
    return ( 
        <div>
            <div className="row">
              <div className="offset-lg-3 col-lg-6">
                <form className="container" onSubmit={handlesubmit}>
                    <div className="card" style={{"textAlign":"left"}}>
                        <div className="card-title">
                            <h2>employee Create</h2>
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
                                        <label>Date Of Birth</label>
                                        <input required type="date" value={dateofbirth} onChange={e=>dateofbirthchange(e.target.value)} className="form-control"></input>
                                    </div>
                                </div>
                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>Salary</label>
                                        <input required value={salary} onChange={e=>salarychange(e.target.value)} className="form-control"></input>
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
export default EmpCreate;