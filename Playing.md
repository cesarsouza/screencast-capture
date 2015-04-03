# Introduction #

Screencast Capture always record videos using H624. To play videos generated with Screencast Capture, you will need to have the H624 codec installed. However, most likely you should already have one installed in your system and the videos should play directly on your favorite media player (including Windows Media Player) without requiring any extra steps. However, there may be the case where your player may refuse to play the video. In this case, you may either be missing the correct codec or your playing is not locating the appropriate codec correctly.

If you feel the problem may lie within your player, download and use an alternate player such as [Media Player Classic](http://mpc-hc.sourceforge.net/) or [VLC](http://www.videolan.org/vlc/index.html). Otherwise, a good way to ensure you have all proper codecs is to install [ffdshow](http://en.wikipedia.org/wiki/Ffdshow), a free media encoder/decoder for Windows which is able to play most existent video formats with ease.


# ffdshow #

ffdshow is a media decoder for a really extensive list of video and audio codecs. Instead of installing separate decoders and encoders for each codec, ffdshow provides support for near all video and audio formats out-of-the-box. It is free software, released under the GPL, runs on Windows and features a neat installer.

ffdshow is currently being under development under the [ffdshow-tryouts project](http://ffdshow-tryout.sourceforge.net/). Installation is quick-and-easy, and the default installer settings should suffice. After installation, all videos should be playable using any media player which uses DirectShow, such as Windows Media Player.
<br />
<p align='center'>
<img src='https://screencast-capture.googlecode.com/svn/wiki/images/codecs/ffdshow-1.png' align='center' /></p>

<br />


### I have installed ffdshow but still can't play the videos ###

If you find yourself in this situation, chances are you are using Windows 7 32-bit. If the videos refuse to play even after installing ffdshow, it may be necessary to use [Win7DSFilterTweaker](http://www.codecguide.com/windows7_preferred_filter_tweaker.htm) to enforce select ffdshow as the default decoding mechanism. To achieve this, download and execute Filter Tweaker. It is a standalone executable and does not needs to be installed.

<table>
<blockquote><tr>
<blockquote><td>
<blockquote><img src='https://screencast-capture.googlecode.com/svn/wiki/images/codecs/tweaker.png' /></td>
</blockquote><td>
<blockquote><img src='https://screencast-capture.googlecode.com/svn/wiki/images/codecs/tweaker-mf.png' /></td>
</blockquote></blockquote></tr>
</table></blockquote>

After running the application, click the "Media Foundation" option, and in the next screen, mark the first checkbox to disable media foundation. After clicking _Apply_, the videos should start playing as normal in Windows Media Player.