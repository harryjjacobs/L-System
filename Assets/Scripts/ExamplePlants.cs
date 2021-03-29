public class ExamplePlants {
    public LSystem Tree = new LSystem("0", new Rule[] {
            new Rule('1', "1"), //OR for a bigger trunk, make the second value '11' instead of just '1'
            new Rule('0', "11[-0]+0") //Add more 1's to the front to increase branch size
    });

    public LSystem FractalPlant = new LSystem("X", new Rule[] {
            new Rule('1', "1"), //OR for a bigger trunk, make the second value '11' instead of just '1'
            new Rule('0', "11[-0]+0") //Add more 1's to the front to increase branch size
    });
}
