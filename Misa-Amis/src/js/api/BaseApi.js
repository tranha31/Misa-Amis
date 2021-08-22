import Repository from "./Repository";

export default class BaseAPI {

    constructor() {
        this.controler = null;
    }
    /**
     * Phương thức lấy tất cả dữ liệu
     * create by: TQHa (4/8/2021)
     */
    getAll() {
        return Repository.get(`${this.controler}`);
    }
    /**
     * Lấy đối tượng theo id
     * create by: TQHa (4/8/2021)
     * @param {*} id 
     */
    getById(id){
        return Repository.get(`${this.controler}/${id}`);
    }
    /**
     * Thêm mới dữ liệu
     * create by: TQHa (4/8/2021)
     * @param {*} body 
     * @returns 
     */
    addNew(body){
        return Repository.post(`${this.controler}`, body);
    }
    /**
     * Hàm cập nhật dữ liệu
     * create by: TQHa (4/8/2021)
     * @param {*} body 
     */
    update(body) {
        return Repository.put(`${this.controler}`, body);
    }
    /**
     * Hàm xóa bản ghi
     * create by: TQHa (4/8/2021)
     * @param {*} listId danh sách id 
     */
    delete(listId) {
        return Repository.delete(`${this.controler}`, {data: listId});
    }
}