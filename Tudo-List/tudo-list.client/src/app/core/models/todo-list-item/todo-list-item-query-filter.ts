import { PriorityEnum } from "../../enums/priority-enum";
import { StatusEnum } from "../../enums/status-enum";

export interface TodoListItemQueryFilter {
    title?: string;
    status?: StatusEnum;
    priority?: PriorityEnum;
    creationDate?: Date;
    initialDate?: Date;
    finalDate?: Date;
}