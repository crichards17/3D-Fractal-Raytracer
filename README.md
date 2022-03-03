# BoxRayTracer

## Description
This application is a CPU-side 3D rendering engine with accompanying WPF front-end. Allows for static visualization of multiple objects and lights in a given scene, with configurable parameters for objects, lighting, and camera. 

## Purpose
This application is being developed with twofold purpose:
1. As a learning opportunity. This application's development has so far required me to either expand my prior knowledge or learn from scratch in the followign areas:
    * C# development
    * Top-down object-oriented design and solution structure
    * 3D rendering via Distance Estimation (and associated vector math)
    * Lighting principles and implementation
    * ...and more to come!
2. The rendering backend is being built to accomodate a subsequent effort which will implement a GPU-side rendering pipeline.

## Technologies/principles used
* C#
* Distance Estimation based raytracing model
* Blinn-Phong lighting model for ambient, diffuse, and specular components

## Development
This portion of this document will serve as a development journal of sorts, for convenient review by myself and others.

### C# Practice

My first endeavor, before beginning development on the ray tracer, was to tackle a few smaller C# projects to familiarize myself with C# syntax (coming from Java previously), Solution structure, WPF, and other initial aspects of C# development. These projects can be found in my ["Csharp-Practice" repo](https://github.com/crichards17/Csharp-Practice).

### WPF Setup and Bitmap generation

The first objective was to set up the WPF app, as well as much of the initial rendering backend and Bitmap pipeline. The milestone here was the ability to returna an image from the raytracer (in this case, returning a single color value for all pixels) and displaying it on the front end.

<img src="https://github.com/crichards17/BoxRayTracer/blob/main/Progression/Captures/02-13_Red_Box.PNG?raw=true" height="300" name="Red_Box" style="display:block;">


### Imaging the first sphere via DE

<img src="https://github.com/crichards17/BoxRayTracer/blob/main/Progression/Captures/02-13_First_Sphere.PNG?raw=true" height="300" name="First_Sphere" style="display:block;">

### Camera and object parameters implemented in the front end

<img src="https://github.com/crichards17/BoxRayTracer/blob/main/Progression/Captures/02-15_Rendering_Controls.PNG?raw=true" height="300" name="Rendering_Controls" style="display:block;">

### Lighting model (Blinn-Phong)

Initial implementation of the lighting model. Light interface and corresponding objects were created, and the raytracer updated to calculate lighting components (ambient, diffuse, specular) from all light sources for a given pixel.
<img src="https://github.com/crichards17/BoxRayTracer/blob/main/Progression/Captures/02-18_First_Lighting.PNG?raw=true" height="300" name="First_Lighting" style="display:block;">
<label for="First_Lighting">Initial lighting implementation</label>

<img src="https://github.com/crichards17/BoxRayTracer/blob/main/Progression/Captures/02-18_Blinn_Specular.PNG?raw=true" height="300" name="Blinn_Specular" style="display:block;">

### Multiple Objects in the scene

<img src="https://github.com/crichards17/BoxRayTracer/blob/main/Progression/Captures/02-19_Multiple_Objects.PNG?raw=true" height="300" name="Multiple_Objects" style="display:block;">

### Shadow Rays

<img src="https://github.com/crichards17/BoxRayTracer/blob/main/Progression/Captures/02-26_First_Shadows.PNG?raw=true" height="300" name="Multiple_Objects" style="display:block;">

### Box DE and Mixed Objects

<img src="https://github.com/crichards17/BoxRayTracer/blob/main/Progression/Captures/03-02_Mixed_Objects.PNG?raw=true" height="300" name="Multiple_Objects" style="display:block;">

