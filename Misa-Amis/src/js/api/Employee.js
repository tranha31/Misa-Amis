import BaseAPI from "./BaseApi";
import Repository from "./Repository";

class Employee extends BaseAPI{
    constructor(){
        super();
        this.controler = "Employee";
    }
    /**
     * Lọc nhân viên
     * create by: TQHa (4/8/2021)
     * @param {object} p: Nội dung lọc 
     */
    filter(p){
        return Repository.get(`${this.controler}/employeeFilter`, {params: p});
    }
    /**
     * Lấy mã nhân viên mới
     * create by: TQHa (4/8/2021)
     */
    getNewCode(){
        return Repository.get(`${this.controler}/newEmployee`);
    }
    
}

export default new Employee();

