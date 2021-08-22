import BaseAPI from "./BaseApi";

class Department extends BaseAPI{
    constructor(){
        super();
        this.controler = "Department";
    }
    
}

export default new Department();