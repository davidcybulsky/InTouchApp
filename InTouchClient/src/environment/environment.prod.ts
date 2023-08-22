import { IEnvoronment } from "./environment.interface";

export class EnvironmentProd implements IEnvoronment{
    serverEndpoint: string = "https://localhost:7211";

}