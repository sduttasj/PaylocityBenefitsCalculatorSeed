import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Link } from "react-router-dom";
const EmpPaycheckView = () => {
    const {empid} = useParams();
    const[empPaycheck,empPaycheckChange] = useState("");
    useEffect(() =>{        
        fetch("https://localhost:7124/api/v1/Paycheck/" + empid).then((res) => {
            return res.json();
        }).then((resp) =>{
            empPaycheckChange(resp.data);
        }).catch((err) => {
            console.log(err.message);
        })
    },[]);
    return ( 
        <div>
            <div>
                <div className="row">
                                <div className="col-lg-12">
                                    <div className="form-group">
                                        <label>PayCheck Biweekly</label>
                                        <input value={empPaycheck} disabled="disabled" className="form-control"></input>
                                    </div>
                                </div>
                </div>
                <Link className="btn btn-danger" to="/">Back to listing</Link>
            </div>

        </div>
    );
}
export default EmpPaycheckView;