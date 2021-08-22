import Employee from "./Employee";
import Department from "./Department";

const repositories = {
    employee : Employee,
    department : Department
};

export const RepositoryFactory = {
    get : name => repositories[name]
}
