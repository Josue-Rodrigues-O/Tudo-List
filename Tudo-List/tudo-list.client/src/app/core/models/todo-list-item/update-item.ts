import { PriorityEnum } from "../../enums/priority-enum";
import { StatusEnum } from "../../enums/status-enum";

export interface UpdateItem {
    id: string;
    title?: string;
    description?: string;
    status?: StatusEnum;
    priority?: PriorityEnum;
}